namespace Ninito.UsualSuspects.WeightedPool
{
    /// <summary>
    ///     A pool that can be drawn from that utilizes chance select its items
    /// </summary>
    /// <typeparam name="T">The type of item in the pool</typeparam>
    public interface IChancePool<out T>
    {
        /// <summary>
        ///     Draws an item from the chance pool
        /// </summary>
        /// <returns>The drawn item</returns>
        public T Draw();
    }
}