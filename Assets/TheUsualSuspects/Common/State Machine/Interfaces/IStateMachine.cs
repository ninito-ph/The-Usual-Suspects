namespace Ninito.UsualSuspects
{
    /// <summary>
    ///     An interface that circumvents Unity's serialization limitations
    /// </summary>
    public interface IStateMachine
    {
        #region Properties

        public State CurrentState { get; set; }
        public State DefaultState { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Transitions to a given state through its ID
        /// </summary>
        /// <param name="stateID">The state ID</param>
        public void TransitionToState(string stateID);

        /// <summary>
        ///     Transitions to a given state through its hash.
        /// </summary>
        /// <param name="stateHash">The state ID Hash</param>
        public void TransitionToState(int stateHash);

        #endregion
    }
}