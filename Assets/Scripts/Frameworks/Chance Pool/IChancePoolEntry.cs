namespace Ninito.UsualSuspects.WeightedPool
{
    /// <summary>
    ///     An interface that defines an entry containing a type T item and chance for a chance pool
    /// </summary>
    /// <typeparam name="T">The entry's item type</typeparam>
    public interface IChancePoolEntry<out T>
    {
        /// <summary>
        ///     The item of the entry
        /// </summary>
        public T Item { get; }
        
        /// <summary>
        ///     The chance of the entry
        /// </summary>
        public int Chance { get; set; }
    }
}