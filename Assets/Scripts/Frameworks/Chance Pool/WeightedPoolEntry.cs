using System;
using UnityEngine;

namespace ManyTools.UnityExtended.WeightedPool
{
    /// <summary>
    ///     A class that contains a specific weighed pool entry
    /// </summary>
    [Serializable]
    public struct WeightedPoolEntry<T> : IChancePoolEntry<T>
    {
        #region Private Fields

        [SerializeField]
        [Tooltip(tooltip: "The item to be drawn")]
        private T item;

        [SerializeField]
        [Tooltip(tooltip: "The item's weight")]
        private int weight;

        #endregion

        #region IChancePoolEntry Implementation

        public T Item => item;
        
        public int Chance
        {
            get => weight;
            set => weight = value;
        }

        #endregion
    }
}