using UnityEngine;

namespace Ninito.UsualSuspects.DataGrid
{
    /// <summary>
    ///     A grid of data that exists spatially
    /// </summary>
    public class SpatialDataGrid<T> : DataGrid<T>
    {
        #region Constructor

        public SpatialDataGrid(int width, int height, float cellSize, Vector3 offset) :
            base(width: width, height: height)
        {
            CellSize = cellSize;
            Offset = offset;
        }

        #endregion

        #region Properties

        public float CellSize { get; }
        public Vector3 Offset { get; set; }

        #endregion

        #region DataGrid Overrides

        public override T this[int xIndex, int yIndex]
        {
            get
            {
                if (IsCellValid(xIndex: xIndex, yIndex: yIndex)) return base[xIndex: xIndex, yIndex: yIndex];

                #if UNITY_EDITOR
                Debug.LogWarning(message: $"Attempted to get value outside grid! ({xIndex}, {yIndex})");
                #endif

                return default;
            }
            set
            {
                if (IsCellValid(xIndex: xIndex, yIndex: yIndex))
                {
                    base[xIndex: xIndex, yIndex: yIndex] = value;
                }
                else
                {
                    #if UNITY_EDITOR
                    Debug.LogWarning(message: $"Attempted to set value outside grid! ({xIndex}, {yIndex})");
                    #endif
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the world position of a grid cell
        /// </summary>
        /// <param name="x">The X index of the cell</param>
        /// <param name="y">The Y index of the cell</param>
        /// <returns>The world position of the cell</returns>
        public virtual Vector3 GetCellWorldPosition(int x, int y)
        {
            return new Vector3(x: x, y: y) * CellSize + Offset;
        }

        /// <summary>
        ///     Gets a cell's coordinates through a given world position
        /// </summary>
        /// <param name="worldPosition">The world position to get the cell coordinates for</param>
        /// <param name="xIndex">The X index at the given coordinates</param>
        /// <param name="yIndex">The Y index at the given coordinates</param>
        public virtual void GetCellIndex(Vector3 worldPosition, out int xIndex, out int yIndex)
        {
            xIndex = Mathf.FloorToInt(f: (worldPosition - Offset).x / CellSize);
            yIndex = Mathf.FloorToInt(f: (worldPosition - Offset).y / CellSize);
        }

        /// <summary>
        ///     Sets a cell's Object through world position
        /// </summary>
        /// <param name="worldPosition">The world position of the cell</param>
        /// <param name="cellObject">The object to set the cell to</param>
        public virtual void SetCellObjectByPosition(Vector3 worldPosition, T cellObject)
        {
            GetCellIndex(worldPosition: worldPosition, xIndex: out int xIndex, yIndex: out int yIndex);
            this[xIndex: xIndex, yIndex: yIndex] = cellObject;
        }

        /// <summary>
        ///     Gets a cell's object through world position
        /// </summary>
        /// <param name="worldPosition">The world position to get the cell from</param>
        /// <returns>The object in the cell at the given world position</returns>
        public virtual T GetCellObjectByPosition(Vector3 worldPosition)
        {
            GetCellIndex(worldPosition: worldPosition, xIndex: out int xIndex, yIndex: out int yIndex);
            return this[xIndex: xIndex, yIndex: yIndex];
        }

        /// <summary>
        ///     Gets whether a specific index is valid within the grid
        /// </summary>
        /// <returns>Whether the given indexes are valid</returns>
        public bool IsCellValid(int xIndex, int yIndex)
        {
            bool xIndexIsWithinBounds = xIndex >= 0 && xIndex < gridObjects.GetLength(dimension: 0);
            bool yIndexIsWithinBounds = yIndex >= 0 && yIndex < gridObjects.GetLength(dimension: 1);
            return xIndexIsWithinBounds && yIndexIsWithinBounds;
        }

        /// <summary>
        ///     Gets whether a specific index is valid within the grid
        /// </summary>
        /// <returns>Whether the given indexes are valid</returns>
        public bool IsCellValid(Vector2Int position)
        {
            bool xIndexIsWithinBounds = position.x >= 0 && position.x < gridObjects.GetLength(dimension: 0);
            bool yIndexIsWithinBounds = position.y >= 0 && position.y < gridObjects.GetLength(dimension: 1);
            return xIndexIsWithinBounds && yIndexIsWithinBounds;
        }

        #endregion
    }
}