using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Ninito.UsualSuspects.Pooling
{
    /// <summary>
    ///     A component that connects to and manages other poolable components
    /// </summary>
    public class PoolMember : MonoBehaviour
    {
        #region Private Fields

        private IPoolable[] poolableComponents;
        private readonly CancellationTokenSource destroyCancelTokenSource = new CancellationTokenSource();

        #endregion

        #region Properties

        public bool IsSubmerged { get; private set; } = true;

        public Pool MotherPool { get; set; }

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            UpdatePoolableComponents();
        }

        private void OnDestroy()
        {
            destroyCancelTokenSource.Cancel();
            destroyCancelTokenSource.Dispose();
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Updates the list of poolable components
        /// </summary>
        public void UpdatePoolableComponents()
        {
            poolableComponents = GetComponentsInChildren<IPoolable>();
        }

        /// <summary>
        ///     Submerges the Poolable object into the pool.
        /// </summary>
        public void Return()
        {
            SetSubmersionState(submerged: true);
            NotifyPoolableComponents(returned: true);
        }

        /// <summary>
        ///     Submerges the Poolable object into the pool with a delay.
        /// </summary>
        /// <param name="delay">The delay in seconds to wait before submerging the object</param>
        public async void ReturnDelayed(float delay)
        {
            try
            {
                await Task.Delay(millisecondsDelay: (int) (delay * 1000),
                    cancellationToken: destroyCancelTokenSource.Token);
            }
            catch
            {
                // ignored
            }

            Return();
        }

        /// <summary>
        ///     Emerges the Poolable object from the pool
        /// </summary>
        /// <param name="position">The position at which to emerge the object</param>
        /// <param name="rotation">The rotation to emerge the object with</param>
        public void Take(Vector3 position, Quaternion rotation)
        {
            RepositionTransform(position: position, rotation: rotation);
            SetSubmersionState(submerged: false);
            NotifyPoolableComponents(returned: false);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Repositions the transform of the member
        /// </summary>
        /// <param name="position">The new position of the member</param>
        /// <param name="rotation">The new rotation of the member</param>
        private void RepositionTransform(Vector3 position, Quaternion rotation)
        {
            Transform objectTransform = transform;

            objectTransform.position = position;
            objectTransform.rotation = rotation;
        }

        /// <summary>
        ///     Sets the submersion state of the member in the pool, and performs related operations
        /// </summary>
        /// <param name="submerged">Whether the member is submerged</param>
        private void SetSubmersionState(bool submerged)
        {
            IsSubmerged = submerged;
            gameObject.SetActive(value: !IsSubmerged);
            MotherPool.CurrentEmerged = submerged ? MotherPool.CurrentEmerged-- : MotherPool.CurrentEmerged++;
        }

        /// <summary>
        ///     Notifies a poolable components that the member has been returned or taken from the pool
        /// </summary>
        /// <param name="returned">Whether the member has been returned (or taken)</param>
        private void NotifyPoolableComponents(bool returned)
        {
            if (returned)
            {
                for (int index = 0, upper = poolableComponents.Length; index < upper; index++)
                {
                    poolableComponents[index].OnReturn();
                }
            }
            else
            {
                for (int index = 0, upper = poolableComponents.Length; index < upper; index++)
                {
                    poolableComponents[index].OnTake();
                }
            }
        }

        #endregion
    }
}