using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ManyTools.UnityExtended.Pooling
{
    /// <summary>
    ///     A pool of objects that can be reused
    /// </summary>
    public class Pool
    {
        #region Private Fields

        private readonly GameObject template;

        private int currentEmerged;
        private int peakEmerged;
        private int?[] memberUsagePeaks;

        #endregion

        #region Properties

        // ReSharper disable once MemberCanBePrivate.Global
        public List<PoolMember> Members { get; } = new List<PoolMember>();

        public int CurrentEmerged
        {
            get => currentEmerged;
            set
            {
                currentEmerged = value;
                peakEmerged = currentEmerged > peakEmerged ? currentEmerged : peakEmerged;
            }
        }

        public PoolSettings Settings { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new pool with no limit
        /// </summary>
        /// <param name="template">The template GameObject of the pool</param>
        /// <param name="poolSettings">Data about the pool's setup</param>
        public Pool(GameObject template, PoolSettings poolSettings)
        {
            this.template = template;
            Settings = poolSettings;
            Add(amount: poolSettings.preFillAmount);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets whether the pool is over its member limit
        /// </summary>
        /// <returns>Whether the pool is over its member limit</returns>
        public bool IsOverMemberLimit()
        {
            return Members.Count > Settings.poolLimit;
        }

        /// <summary>
        ///     Removes submerged poolables until the pool is at or under its limit
        /// </summary>
        public void CullExcessMembers()
        {
            // De-Pool members until the pool is at or under its limit
            for (int index = Members.Count - 1; index >= 0; index--)
            {
                // Remove the poolable only if he is submerged
                if (Members[index: index].IsSubmerged)
                {
                    RemoveMember(indexInPool: index);
                }

                // Check if the pool is at or under its limit. If so, end the loop here
                if (!IsOverMemberLimit())
                {
                    break;
                }
            }
        }

        /// <summary>
        ///     Removes all objects from the pool
        /// </summary>
        /// <param name="destroy">Whether to destroy pool objects before removal</param>
        public void Wipe(bool destroy)
        {
            for (int index = Members.Count - 1; index >= 0; index--)
            {
                RemoveMember(indexInPool: index, destroy: destroy);
            }
        }

        /// <summary>
        ///     Mass adds objects to the pool
        /// </summary>
        /// <param name="amount">The amount of objects to add</param>
        /// <returns>The last object added to the pool</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public PoolMember Add(int amount)
        {
            PoolMember latestAdded = null;

            for (int index = 0; index < amount; index++)
            {
                // Instantiates the object
                Object.Instantiate(original: template, position: Vector3.zero, rotation: Quaternion.identity)
                    .TryGetComponent(component: out latestAdded);

                latestAdded.MotherPool = this;

                // Adds it to the list
                Members.Add(item: latestAdded);
            }

            return latestAdded;
        }

        /// <summary>
        ///     Gets a submerged member from the pool
        /// </summary>
        /// <returns>A submerged member</returns>
        public PoolMember GetSubmergedMember()
        {
            // Tries getting the earliest submerged poolable
            for (int index = 0, upper = Members.Count; index < upper; index++)
            {
                if (!Members[index: index].IsSubmerged) continue;
                return Members[index: index];
            }

            // If none was available, create a new one and return it
            return Add(amount: 1);
        }

        /// <summary>
        ///     Adapts the pool's limit member count
        /// </summary>
        public void AdaptMemberLimit()
        {
            if (memberUsagePeaks == null)
            {
                InitializeMemberUsagePeaks();
            }

            UpdateMemberUsagePeaks();

            Settings.poolLimit = Mathf.Max(a: GetAverageMemberUsage(), b: Settings.minimumLimit);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Removes an object from the pool
        /// </summary>
        /// <param name="indexInPool">The index of the object in the pool</param>
        /// <param name="destroy">Whether the object should be destroyed after removal</param>
        private void RemoveMember(int indexInPool, bool destroy = true)
        {
            if (destroy)
            {
                Object.Destroy(obj: Members[index: indexInPool]);
            }

            Members.RemoveAt(index: indexInPool);
        }

        /// <summary>
        ///     Creates and fills the MemberUsagePeaks array with default values
        /// </summary>
        private void InitializeMemberUsagePeaks()
        {
            memberUsagePeaks = new int?[Settings.intervalMemory];

            for (int index = 0, upper = memberUsagePeaks.Length; index < upper; index++)
            {
                memberUsagePeaks[index] = Settings.poolLimit;
            }
        }

        /// <summary>
        ///     Samples, updates and culls the interval peak array
        /// </summary>
        private void UpdateMemberUsagePeaks()
        {
            Array.Copy(sourceArray: memberUsagePeaks, sourceIndex: 1, destinationArray: memberUsagePeaks,
                destinationIndex: 0,
                length: memberUsagePeaks.Length - 1);

            memberUsagePeaks[memberUsagePeaks.Length - 1] = peakEmerged;
        }

        /// <summary>
        ///     Gets the average member usage given the peak intervals
        /// </summary>
        /// <returns>The average member usage of the current peak intervals</returns>
        private int GetAverageMemberUsage()
        {
            if (memberUsagePeaks == null)
            {
                InitializeMemberUsagePeaks();
            }

            // ReSharper disable once PossibleInvalidOperationException
            return Mathf.CeilToInt(f: (float) memberUsagePeaks.Average());
        }

        #endregion
    }
}