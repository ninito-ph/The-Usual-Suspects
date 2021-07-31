using System.Collections.Generic;
using UnityEngine;

namespace ManyTools.UnityExtended.WeightedPool
{
    /// <summary>
    ///     A pool of objects with weights
    /// </summary>
    /// <typeparam name="T">The type of the object to draw from</typeparam>
    public class WeightedPool<T> : ScriptableObject, IChancePool<T>
    {
        #region Private Fields

        [Header(header: "Pool Config")]
        [SerializeField]
        [Tooltip(tooltip: "The object and its weight")]
        private List<IChancePoolEntry<T>> pool = new List<IChancePoolEntry<T>>();

        #endregion

        #region Properties

        public List<IChancePoolEntry<T>> Pool => pool;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Draws an item from the weighted pool
        /// </summary>
        /// <returns>The drawn item</returns>
        public T Draw()
        {
            int pickedWeight = Random.Range(minInclusive: 0, maxExclusive: GetTotalWeight());

            for (int index = 0, upper = Pool.Count; index < upper; index++)
            {
                if (pickedWeight < Pool[index: index].Chance)
                {
                    return Pool[index: index].Item;
                }

                pickedWeight -= Pool[index: index].Chance;
            }

            Debug.LogError(message: "The weighed random algorithm failed! This should never happen." +
                                    " Maybe the pool is empty?");
            return default;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the total weight of the pool
        /// </summary>
        /// <returns>The total weight of the pool</returns>
        private int GetTotalWeight()
        {
            int totalWeight = 0;

            for (int index = 0, upper = Pool.Count; index < upper; index++) totalWeight += Pool[index: index].Chance;

            return totalWeight;
        }

        #endregion
    }
}