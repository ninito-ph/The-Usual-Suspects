namespace ManyTools.UnityExtended.ProgressBar
{
    /// <summary>
    /// An interface that defines something with measurable progress 
    /// </summary>
    public interface IMeasurableProgress
    {
        /// <summary>
        /// The minimum progress amount. Can be used to define offsets to the progress.
        /// </summary>
        public float MinimumProgress { get; }
        
        /// <summary>
        /// The current progress amount
        /// </summary>
        public float CurrentProgress { get; }
        
        /// <summary>
        /// The maximum progress amount
        /// </summary>
        public float MaxProgress { get; }
    }
}