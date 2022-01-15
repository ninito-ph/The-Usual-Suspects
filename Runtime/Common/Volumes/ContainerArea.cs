using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ninito.UsualSuspects.Volumes
{
    /// <summary>
    ///     A trigger area that keeps track of which items are in it
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class ContainerArea : MonoBehaviour
    {
        #region Private Fields

        private Collider2D _volume;
        private readonly HashSet<Collider2D> _collidersInVolume = new HashSet<Collider2D>();

        #endregion

        #region Properties

        public HashSet<Collider2D> CollidersInVolume
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
            
            Debug.LogWarning("Container Area's collider is missing or not marked as a trigger!");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            AddColliderToList(other);
        }

        private void OnTriggerExit2D(Collider2D other)
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

        private void RemoveColliderFromList(Collider2D colliderToRemove)
        {
            CollidersInVolume.Remove(colliderToRemove);
            ColliderLeftVolume?.Invoke();
        }

        private void AddColliderToList(Collider2D colliderToAdd)
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