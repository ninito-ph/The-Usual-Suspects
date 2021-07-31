using System;
using UnityEngine;

namespace ManyTools.UnityExtended
{
    /// <summary>
    ///     A state read and performed by a <see cref="StateMachine" />
    /// </summary>
    public abstract class State : ScriptableObject, IState
    {
        #region Private Fields

        private int stateHash;

        #endregion

        #region Protected Fields

        public IStateMachine StateMachine { get; set; }

        #endregion

        #region IState Implementation

        public string StateID => GetType().Name;

        public int StateHash => GetStateIDHash();

        /// <summary>
        ///     Runs when the state is entered
        /// </summary>
        public abstract void Enter();

        /// <summary>
        ///     Runs at every update of the state
        /// </summary>
        public abstract void Update();

        /// <summary>
        ///     Runs when the state exits
        /// </summary>
        public abstract void Exit();

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the integer hash of a state's ID
        /// </summary>
        /// <returns>The hash of the state's ID</returns>
        private int GetStateIDHash()
        {
            if (stateHash != default) return stateHash;

            if (String.IsNullOrEmpty(value: StateID))
            {
                Debug.LogError(message: "A state's state ID was empty! All IDs must be non-empty and unique");
            }

            return stateHash = StateID.GetHashCode();
        }

        #endregion
    }
}