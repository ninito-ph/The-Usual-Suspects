using System;
using UnityEngine;

namespace Ninito.UsualSuspects.Pooling
{
    /// <summary>
    ///     A struct that holds data for display in the editor about default pools
    /// </summary>
    [Serializable]
    public class PoolSettings
    {
        [Header(header: "Pool Settings")]
        [Tooltip(tooltip: "The ideal limit of members in the pool.")]
        public int poolLimit;

        [Tooltip(tooltip: "How many objects to preemptively add to the pool.")]
        public int preFillAmount;

        [Tooltip(tooltip: "Whether the pool should periodically cull members over the pool's limit")]
        public bool cullExcessMembers;

        [Tooltip(tooltip: "Whether the pool's limit should change periodically to reflect its usage")]
        public bool adaptivePoolLimits;

        [Tooltip(tooltip: "How many intervals will be recorded to calculate the adaptive limit")]
        public int intervalMemory;

        [Tooltip(tooltip: "The minimum pool limit. The adaptive pool limit will never go below this point")]
        public int minimumLimit;

        public PoolSettings(int poolLimit = 0, int preFillAmount = 0, bool cullExcessMembers = false, bool
            adaptivePoolLimits = false, int minimumLimit = 0, int intervalMemory = 0)
        {
            this.poolLimit = poolLimit;
            this.preFillAmount = preFillAmount;
            this.cullExcessMembers = cullExcessMembers;
            this.adaptivePoolLimits = adaptivePoolLimits;
            this.minimumLimit = minimumLimit;
            this.intervalMemory = intervalMemory;
        }
    }
}