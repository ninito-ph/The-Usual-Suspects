using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ManyTools.UnityExtended.Volumes
{
    /// <summary>
    ///     A trigger volume that keeps track of which items are in it
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ContainerVolume : MonoBehaviour
    {
        #region Private Fields

        private Collider _volume;
        private readonly HashSet<Collider> _collidersInVolume = new HashSet<Collider>();

        #endregion

        #region Properties

        public HashSet<Collider> CollidersInVolume
        {
            get
            {
                RemoveNullCollidersFromList();
                return _collidersInVolume;
            }
        }

        public Action ColliderEnteredVolume { get; set; }
        public Action ColliderLeftVolume { get; set; }

        #endregion

        #region Unity Callbacks

        private void Reset()
        {
            TryGetComponent(out _volume);

            if (_volume != null && _volume.isTrigger) return;
            
            Debug.LogWarning("Smart Trigger Volume's collider is missing or not marked as a trigger!");
        }

        private void OnTriggerEnter(Collider other)
        {
            AddColliderToList(other);
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveColliderFromList(other);
        }

        #endregion

        #region Public Methods

        public IEnumerable<T> GetComponentsInVolume<T>()
        {
            return CollidersInVolume.Select(col => col.gameObject.GetComponent<T>())
                .Where(component => component != null);
        }

        #endregion

        #region Private Methods

        private void RemoveColliderFromList(Collider colliderToRemove)
        {
            CollidersInVolume.Remove(colliderToRemove);
            ColliderLeftVolume?.Invoke();
        }

        private void AddColliderToList(Collider colliderToAdd)
        {
            if (!CollidersInVolume.Contains(colliderToAdd))
            {
                CollidersInVolume.Add(colliderToAdd);
            }
            
            ColliderEnteredVolume?.Invoke();
        }

        private void RemoveNullCollidersFromList()
        {
            _collidersInVolume.RemoveWhere(col => col == null);
        }

        #endregion
    }
}