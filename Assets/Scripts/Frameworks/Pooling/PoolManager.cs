using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ninito.UsualSuspects.Pooling
{
    /// <summary>
    ///     A class that autonomously manages and provides pools and pooled objects
    /// </summary>
    public class PoolManager : Singleton<PoolManager>
    {
        #region Private Fields

        [Header(header: "Pooling Parameters")]
        [SerializeField]
        [Tooltip(tooltip: "The settings for when a new pool is created implicitly")]
        private PoolSettings defaultPoolSettings;

        [Header(header: "Automatic Pool Culling")]
        [SerializeField]
        [Tooltip(tooltip: "How frequently should pools cull excess members")]
        [Range(min: 1, max: 120)]
        private int cullInterval = 40;

        [Header(header: "Adaptive Pool Limiting")]
        [SerializeField]
        [Tooltip(tooltip: "How frequently pools should adapt their limit")]
        [Range(min: 1, max: 120)]
        private int adaptInterval = 40;

        [Header(header: "Default Pools")]
        [SerializeField]
        [Tooltip(tooltip: "What pools should be preemptively created")]
        private SerializedDictionary<GameObject, PoolSettings> defaultPools =
            new SerializedDictionary<GameObject, PoolSettings>();

        private readonly Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();
        private Coroutine cullRoutine;
        private Coroutine adaptRoutine;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            CreateDefaultPools();

            cullRoutine = StartCoroutine(routine: CullPoolExcessMembers());
            adaptRoutine = StartCoroutine(routine: AdaptPoolLimits());
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Adapts the member limit in all adaptive pools
        /// </summary>
        private IEnumerator AdaptPoolLimits()
        {
            // Caches interval
            var intervalCache = new WaitForSecondsRealtime(time: adaptInterval);
            yield return intervalCache;

            // Loops forever
            while (true)
            {
                var adaptivePools = pools.Values.Where(predicate: pool => pool.Settings.adaptivePoolLimits);

                // Iterates through every pool
                foreach (Pool pool in adaptivePools)
                {
                    pool.AdaptMemberLimit();
                }

                yield return intervalCache;
            }
        }


        /// <summary>
        ///     Removes submerged poolables in all pools over their limit
        /// </summary>
        private IEnumerator CullPoolExcessMembers()
        {
            // Caches interval
            var intervalCache = new WaitForSecondsRealtime(time: cullInterval);
            yield return intervalCache;

            // TODO: Check if this is a good idea. Probably not, could cause a memory leak
            while (true)
            {
                var overflowingPools =
                    pools.Values.Where(predicate: pool => pool.IsOverMemberLimit() && pool.Settings.cullExcessMembers);

                foreach (Pool pool in overflowingPools)
                {
                    pool.CullExcessMembers();
                }

                yield return intervalCache;
            }
        }

        /// <summary>
        ///     Creates all default pools
        /// </summary>
        private void CreateDefaultPools()
        {
            foreach (var defaultPool in defaultPools)
            {
                CreatePool(poolType: defaultPool.Key, poolSettings: defaultPool.Value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates and adds a new pool to the manager
        /// </summary>
        /// <param name="poolType">The GameObject type of the pool</param>
        /// <param name="poolSettings">The settings of the pool</param>
        // ReSharper disable once MemberCanBePrivate.Global
        public Pool CreatePool(GameObject poolType, PoolSettings poolSettings)
        {
            var newPool = new Pool(template: poolType, poolSettings: poolSettings);
            pools.Add(key: poolType, value: newPool);

            return newPool;
        }

        /// <summary>
        ///     Destroys an existing pool
        /// </summary>
        /// <param name="poolType">The pool's key</param>
        public void DestroyPool(GameObject poolType)
        {
            // If the pool doesn't exist, do nothing
            if (!pools.ContainsKey(key: poolType)) return;

            pools[key: poolType].Wipe(destroy: true);

            // Detaches the pool from the manager
            pools.Remove(key: poolType);
        }

        /// <summary>
        ///     Requests a poolable object from a pool
        /// </summary>
        public PoolMember Request(GameObject memberType)
        {
            return pools.ContainsKey(key: memberType)
                ? pools[key: memberType].GetSubmergedMember()
                : CreatePool(poolType: memberType, poolSettings: defaultPoolSettings).GetSubmergedMember();
        }

        #endregion
    }
}