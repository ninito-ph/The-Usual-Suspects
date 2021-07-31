namespace ManyTools.UnityExtended.SaveSystem
{
    /// <summary>
    ///     An interface that makes any component easily saveable
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        ///     Called before the object gets saved
        /// </summary>
        public void BeforeSave();

        /// <summary>
        ///     Called after the object gets loaded
        /// </summary>
        public void AfterLoad();

        /// <summary>
        ///     Checks whether the object should save its data
        /// </summary>
        /// <returns>Whether the object should save its data</returns>
        public bool ShouldSave { get; }

        /// <summary>
        ///     Checks whether the object should load data
        /// </summary>
        /// <returns>Whether the object should load data</returns>
        public bool ShouldLoad { get; }

        /// <summary>
        ///     Gets the unique ID for this MonoBehaviour, a combination of its scene name, its name and its type
        /// </summary>
        /// <returns>The unique ID for this MonoBehaviour</returns>
        // ReSharper disable once InconsistentNaming
        public string UID { get; }
    }
}