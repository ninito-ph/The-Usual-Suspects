using System.Linq;
using ManyTools.UnityExtended.CommonExtensions;
using UnityEngine;

namespace ManyTools.UnityExtended
{
    /// <summary>
    ///     A template state machine pattern
    /// </summary>
    public class StateMachine : MonoBehaviour, IStateMachine
    {
        #region Protected Fields

        // BUG: For some reason, this script's inspector draw time is 14ms.

        [SerializeField]
        protected State[] states;

        #endregion

        #region Properties

        public State CurrentState { get; set; }
        public State DefaultState => states[0];

        #endregion

        #region Unity Callbacks

        protected virtual void Start()
        {
            InitializeStateMachine();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            CurrentState.Update();
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Transitions to a given state through its ID
        /// </summary>
        /// <param name="stateID">The state ID</param>
        public void TransitionToState(string stateID)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = states.First(predicate: state => state.StateID == stateID);

            CurrentState.Enter();
        }

        /// <summary>
        ///     Transitions to a given state through its hash.
        /// </summary>
        /// <param name="stateHash">The state ID Hash</param>
        public void TransitionToState(int stateHash)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = states.First(predicate: state => state.StateHash == stateHash);

            CurrentState.Enter();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Tests if any states have identical hashes
        /// </summary>
        private void CheckForHashCollisions()
        {
            if (states.Length == states.Distinct().Count()) return;

            Debug.LogError(message: "A hash collision was detected in the State Machine! Ensure every" +
                                    " state ID is unique and that there are no duplicate states!");
        }

        /// <summary>
        ///     Initializes all states with necessary information
        /// </summary>
        protected virtual void InitializeStates()
        {
            for (int index = 0, upper = states.Length; index < upper; index++)
            {
                states[index] = MakeStateInstance(state: states[index]);
            }
        }

        /// <summary>
        ///     Makes an instance of the given state linked to this state machine
        /// </summary>
        /// <param name="state">The state to make an instance of</param>
        /// <returns>A linked instance of the given state</returns>
        protected virtual State MakeStateInstance(State state)
        {
            State clonedState = state.Clone();
            clonedState.StateMachine = this;
            return clonedState;
        }

        /// <summary>
        ///     Initializes the state machine, performing safety checks and transitioning to the initial state
        /// </summary>
        /// <returns>Whether the state machine initialized successfully</returns>
        protected virtual bool InitializeStateMachine()
        {
            InitializeStates();
            CheckForHashCollisions();
            TransitionToState(stateHash: states[0].StateHash);

            if (states.Length > 0 && CurrentState != null) return true;

            Debug.LogError(message: "The state machine has no states or could not default to the first" +
                                    " state!");
            return false;
        }

        #endregion
    }
}