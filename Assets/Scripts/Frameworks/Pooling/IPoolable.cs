using UnityEngine;

namespace ManyTools.UnityExtended.Pooling
{
    /// <summary>
    ///     An interface that describes a poolable component
    /// </summary>
    public interface IPoolable
    {
        #region Properties

        public bool IsSubmerged { get; set; }
        public Pool MotherPool { get; set; }
        public GameObject PooledObject { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Called once the poolable is returned to its mother pool
        /// </summary>
        public void OnReturn();

        /// <summary>
        ///     Called once the poolable is taken from the mother pool
        /// </summary>
        public void OnTake();

        #endregion
    }
}