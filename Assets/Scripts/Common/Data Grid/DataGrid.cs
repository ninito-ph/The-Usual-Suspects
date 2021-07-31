using System;
using System.Collections;
using System.Linq;

namespace ManyTools.UnityExtended.DataGrid
{
    /// <summary>
    ///     A grid of data
    /// </summary>
    public class DataGrid<T> : ICollection
    {
        #region Protected Fields

        protected int width;
        protected int height;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        protected T[,] gridObjects;

        #endregion

        #region Properties

        public Action<int, int> CellUpdated { get; set; }
        public T[] FlattenedGrid => gridObjects.Cast<T>().ToArray();
        public int Width => width;
        public int Height => height;

        #endregion

        #region Constructor

        // ReSharper disable once MemberCanBeProtected.Global
        public DataGrid(int width, int height)
        {
            this.width = width;
            this.height = height;

            gridObjects = new T[width, height];
        }

        public DataGrid(int width, int height, Func<int, int, T> perCellInitialization)
        {
            this.width = width;
            this.height = height;

            gridObjects = new T[width, height];
            InitializePerCell(perCellInitialization: perCellInitialization);
        }

        #endregion

        #region ICollection Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            var flatGrid = FlattenedGrid;

            for (int destinationIndex = index, copyIndex = 0; destinationIndex < array.Length; destinationIndex++)
            {
                array.SetValue(value: flatGrid[copyIndex], index: destinationIndex);
                copyIndex++;
            }
        }

        public int Count => Width * Height;

        // TODO: implement proper synchronization
        public bool IsSynchronized { get; }
        public object SyncRoot { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets and gets values
        /// </summary>
        /// <param name="xIndex">The x coordinate index to retrieve from</param>
        /// <param name="yIndex">The x coordinate index to retrieve from</param>
        public virtual T this[int xIndex, int yIndex]
        {
            get => gridObjects[xIndex, yIndex];
            // ReSharper disable once MemberCanBeProtected.Global
            set
            {
                gridObjects[xIndex, yIndex] = value;
                CellUpdated?.Invoke(arg1: xIndex, arg2: yIndex);
            }
        }

        /// <summary>
        ///     Executes an action in every cell
        /// </summary>
        /// <param name="perCellAction">
        ///     An action that receives the X and Y coordinates of the cell, as well
        ///     as its value.
        /// </param>
        public void ExecuteActionPerCell(Action<int, int, T> perCellAction)
        {
            for (int xIndex = 0; xIndex < gridObjects.GetLength(dimension: 0); xIndex++)
            {
                for (int yIndex = 0; yIndex < gridObjects.GetLength(dimension: 1); yIndex++)
                {
                    perCellAction(arg1: xIndex, arg2: yIndex, arg3: gridObjects[xIndex, yIndex]);
                }
            }
        }

        /// <summary>
        ///     Initializes all cells in the grid
        /// </summary>
        /// <param name="perCellInitialization">A function that receives the X and Y coordinates of the cell</param>
        public void InitializePerCell(Func<int, int, T> perCellInitialization)
        {
            for (int xIndex = 0; xIndex < gridObjects.GetLength(dimension: 0); xIndex++)
            {
                for (int yIndex = 0; yIndex < gridObjects.GetLength(dimension: 1); yIndex++)
                {
                    gridObjects[xIndex, yIndex] = perCellInitialization(arg1: xIndex, arg2: yIndex);
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the enumerator for the grid
        /// </summary>
        /// <returns>A new GridEnumerator</returns>
        private DataGridEnumerator<T> GetEnumerator()
        {
            return new DataGridEnumerator<T>(grid: gridObjects);
        }

        #endregion
    }
}