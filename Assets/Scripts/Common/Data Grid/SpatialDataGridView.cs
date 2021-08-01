using TMPro;
using System;
using Ninito.UsualSuspects.Utilities;
using UnityEngine;

namespace Ninito.UsualSuspects.DataGrid
{
    /// <summary>
    ///     A class that provides a view into a spatial grid
    /// </summary>
    public class SpatialDataGridView<T>
    {
        #region Private Fields

        private readonly SpatialDataGrid<T> _spatialDataGrid;
        private TMP_Text[,] _cellDisplays;
        private readonly Transform _parentObject;

        #endregion

        #region Properties

        private Vector3 HalfCellSize => new Vector3(x: _spatialDataGrid.CellSize, y: _spatialDataGrid.CellSize) * 0.5f;
        private Vector3 ParentOffset => _parentObject.position;

        #endregion

        #region Constructor

        public SpatialDataGridView(SpatialDataGrid<T> spatialDataGrid, Transform parentObject = null)
        {
            _spatialDataGrid = spatialDataGrid;
            _parentObject = parentObject;

            InitializeCellDisplays();
            SubscribeToGridChanges();
            DrawGridView();
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Subscribes to changes in grid cells
        /// </summary>
        private void SubscribeToGridChanges()
        {
            _spatialDataGrid.CellUpdated += UpdateCellDisplay;
        }

        /// <summary>
        ///     Unsubscribes to changes in grid cells
        /// </summary>
        public void UnsubscribeToGridChanges()
        {
            _spatialDataGrid.CellUpdated -= UpdateCellDisplay;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Initializes the cell displays array
        /// </summary>
        private void InitializeCellDisplays()
        {
            _cellDisplays = new TMP_Text[_spatialDataGrid.Width, _spatialDataGrid.Height];
        }

        /// <summary>
        ///     Draws the grid's debug display
        /// </summary>
        private void DrawGridView()
        {
            DrawGridLines();
            DrawCellDisplays();
        }

        /// <summary>
        ///     Updates the display of the cell at the given coordinates
        /// </summary>
        /// <param name="xIndex">The X index of the cell</param>
        /// <param name="yIndex">The Y index of the cell</param>
        private void UpdateCellDisplay(int xIndex, int yIndex)
        {
            if (_spatialDataGrid[xIndex: xIndex, yIndex: yIndex] == null) return;
            _cellDisplays[xIndex, yIndex].text = _spatialDataGrid[xIndex: xIndex, yIndex: yIndex].ToString();
        }

        /// <summary>
        ///     Draws the grid's values
        /// </summary>
        private void DrawCellDisplays()
        {
            for (int xIndex = 0; xIndex < _spatialDataGrid.Width; xIndex++)
            {
                for (int yIndex = 0; yIndex < _spatialDataGrid.Height; yIndex++)
                {
                    GenerateCellTextDisplay(localPosition: _spatialDataGrid.GetCellWorldPosition(x: xIndex, y: yIndex),
                        xIndex: xIndex, yIndex: yIndex);
                }
            }
        }

        /// <summary>
        ///     Generates a text display for a given cell
        /// </summary>
        /// <param name="localPosition">The local position of the display</param>
        /// <param name="xIndex">The X index of the cell</param>
        /// <param name="yIndex">The Y index of the cell</param>
        private void GenerateCellTextDisplay(Vector3 localPosition, int xIndex, int yIndex)
        {
            string cellText = _spatialDataGrid[xIndex: xIndex, yIndex: yIndex] == null
                ? String.Empty
                : _spatialDataGrid[xIndex: xIndex, yIndex: yIndex].ToString();

            _cellDisplays[xIndex, yIndex] =
                TextUtilities.InstantiateWorldText(
                    parent: _parentObject,
                    localPosition: localPosition + HalfCellSize,
                    text: cellText,
                    textColor: Color.white,
                    alignment: TextAlignmentOptions.Center);
        }

        /// <summary>
        ///     Draws the grid's cell lines
        /// </summary>
        private void DrawGridLines()
        {
            DrawAllCellLines();
            DrawOuterGridLines();
        }

        /// <summary>
        ///     Draws lines for all grid cells
        /// </summary>
        private void DrawAllCellLines()
        {
            for (int xIndex = 0; xIndex < _spatialDataGrid.Width; xIndex++)
            {
                for (int yIndex = 0; yIndex < _spatialDataGrid.Height; yIndex++)
                {
                    Vector3 localPositionCache = _spatialDataGrid.GetCellWorldPosition(x: xIndex, y: yIndex);

                    DrawCellLines(localPosition: localPositionCache, xIndex: xIndex, yIndex: yIndex);
                }
            }
        }

        /// <summary>
        ///     Draws the lines for an individual cell (only draws bottom and left line for optimization purposes)
        /// </summary>
        /// <param name="localPosition">The local position of the cell</param>
        /// <param name="xIndex">The X index of the cell</param>
        /// <param name="yIndex">The Y index of the cell</param>
        private void DrawCellLines(Vector3 localPosition, int xIndex, int yIndex)
        {
            Vector3 parentOffsetCache = ParentOffset;
            
            Debug.DrawLine(start: localPosition + parentOffsetCache,
                end: _spatialDataGrid.GetCellWorldPosition(x: xIndex + 1, y: yIndex) + parentOffsetCache,
                color: Color.white, duration: 100f);

            Debug.DrawLine(start: localPosition + parentOffsetCache,
                end: _spatialDataGrid.GetCellWorldPosition(x: xIndex, y: yIndex + 1) + parentOffsetCache,
                color: Color.white, duration: 100f);
        }

        /// <summary>
        ///     Draws the outer lines for the grid (top and right lines)
        /// </summary>
        private void DrawOuterGridLines()
        {
            Vector3 parentOffsetCache = ParentOffset;

            Debug.DrawLine(
                start: _spatialDataGrid.GetCellWorldPosition(x: 0, y: _spatialDataGrid.Height) + parentOffsetCache,
                end: _spatialDataGrid.GetCellWorldPosition(x: _spatialDataGrid.Height, y: _spatialDataGrid.Height) +
                     parentOffsetCache,
                color: Color.white, duration: 100f);

            Debug.DrawLine(
                start: _spatialDataGrid.GetCellWorldPosition(x: _spatialDataGrid.Height, y: 0) + parentOffsetCache,
                end: _spatialDataGrid.GetCellWorldPosition(x: _spatialDataGrid.Height, y: _spatialDataGrid.Height) +
                     parentOffsetCache,
                color: Color.white, duration: 100f);
        }

        #endregion
    }
}