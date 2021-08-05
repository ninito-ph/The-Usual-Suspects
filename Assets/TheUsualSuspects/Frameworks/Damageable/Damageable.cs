using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ninito.UsualSuspects.Damage
{
    [Serializable]
    public class Damageable
    {
        #region Private Fields

        [Header("Damage Mode")]
        [SerializeField]
        private DamageableMode mode;
        
        [SerializeField]
        private float maxDamage;
        
        [Header("Damage Multipliers")]
        [SerializeField]
        private float vulnerabilityMultiplier = 2f;

        [SerializeField]
        private float resistanceMultiplier = 0.5f;

        [SerializeField]
        private List<Type> vulnerableDamageTypes = new List<Type>();

        [SerializeField]
        private List<Type> resistantDamageTypes = new List<Type>();

        [SerializeField]
        private List<Type> immuneDamageTypes = new List<Type>();

        private float _currentDamage;

        #endregion

        #region Constructor

        public Damageable(DamageableMode mode, float maxDamage)
        {
            this.mode = mode;
            MaxDamage = maxDamage;
        }

        #endregion

        #region Properties

        public Action DamageModified { get; set; }
        public Action Died { get; set; }

        public float Health => GetCurrentHealth();
        public float HealthPercentage => GetCurrentHealthPercentage();

        public float MaxDamage
        {
            get => maxDamage;
            set => maxDamage = value;
        }

        // TODO: REMOVE FROM PROJECT ONCE COMPASSO RPG IS DONE
        public bool IsInvincible { get; set; }

        public float CurrentDamage
        {
            get => _currentDamage;
            private set
            {
                _currentDamage = value;
                DamageModified?.Invoke();
            }
        }

        public List<Type> VulnerableDamageTypes => vulnerableDamageTypes;

        public List<Type> ResistantDamageTypes => resistantDamageTypes;

        public List<Type> ImmuneDamageTypes => immuneDamageTypes;

        #endregion

        #region Public Methods

        public void Damage(float amount, Type damageType)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Damage amounts cannot be negative!");
            if (IsInvincible) return;
            
            float effectiveAmount = ApplyReceivedDamageMultipliers(amount, damageType);
            CurrentDamage += effectiveAmount;

            if (IsDead()) Died?.Invoke();
        }

        public void Heal(float amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Heal amounts cannot be negative!");

            CurrentDamage = Mathf.Max(CurrentDamage -= amount, 0);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Applies a multiplier on taken damage based on whether the damageable has immunity, vulnerability or resistance to
        ///     it.
        /// </summary>
        /// <param name="amount">The amount of damage taken</param>
        /// <param name="damageType">The type of damage taken</param>
        /// <returns>The amount of damage taken after all multipliers are applied</returns>
        private float ApplyReceivedDamageMultipliers(float amount, Type damageType)
        {
            if (VulnerableDamageTypes.Contains(damageType))
            {
                return amount * vulnerabilityMultiplier;
            }

            if (ResistantDamageTypes.Contains(damageType))
            {
                return amount * resistanceMultiplier;
            }

            if (ImmuneDamageTypes.Contains(damageType))
            {
                return 0;
            }

            return amount;
        }

        private float GetCurrentHealth()
        {
            return mode switch
            {
                DamageableMode.Depletion => MaxDamage - CurrentDamage,
                DamageableMode.Accumulation => CurrentDamage,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        ///     Gets the current health in percentage
        /// </summary>
        /// <returns>The current health's percentage from 0 to 1</returns>
        private float GetCurrentHealthPercentage()
        {
            return mode switch
            {
                DamageableMode.Depletion => (MaxDamage - CurrentDamage) / MaxDamage,
                DamageableMode.Accumulation => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool IsDead()
        {
            if (mode == DamageableMode.Depletion)
            {
                return CurrentDamage >= MaxDamage;
            }

            return false;
        }

        #endregion

        #region Private Enums

        /// <summary>
        ///     The damage modes that a Damageable can function in
        /// </summary>
        [Serializable]
        public enum DamageableMode
        {
            Depletion,
            Accumulation
        }

        #endregion
    }
}