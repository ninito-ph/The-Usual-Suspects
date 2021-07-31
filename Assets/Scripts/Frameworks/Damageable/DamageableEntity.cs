using System;
using UnityEngine;
using UnityEngine.Events;

namespace ManyTools.UnityExtended.Damage
{
    public class DamageableEntity : MonoBehaviour
    {
        #region Private Fields

        [SerializeField, InspectorName("Damageable Configs")]
        private Damageable damageable;

        [SerializeField, Header("Events")]
        private UnityEvent onDie;
        [SerializeField]
        private bool destroyOnDie;

        #endregion

        #region Properties

        public Damageable Damageable => damageable;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            Died += onDie.Invoke;

            if (destroyOnDie)
            {
                Died += () => Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Died -= onDie.Invoke;
        }

        #endregion
        
        #region Public Methods

        public Action Died
        {
            get => damageable.Died;
            set => damageable.Died = value;
        }

        public void Damage(float amount, Type damageType) => Damageable.Damage(amount, damageType);
        public void Heal(float amount) => Damageable.Heal(amount);

        #endregion
    }
}