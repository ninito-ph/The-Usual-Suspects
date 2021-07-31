namespace ManyTools.UnityExtended
{
    /// <summary>
    ///     An interface that describes basic functionality for a state machine's state
    /// </summary>
    public interface IState
    {
        #region Properties

        public string StateID { get; }
        public int StateHash { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Runs when the state is entered
        /// </summary>
        public void Enter();

        /// <summary>
        ///     Runs at every update of the state
        /// </summary>
        public void Update();

        /// <summary>
        ///     Runs when the state exits
        /// </summary>
        public void Exit();

        #endregion
    }
}