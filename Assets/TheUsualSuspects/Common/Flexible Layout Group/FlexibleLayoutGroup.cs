using UnityEngine;
using UnityEngine.UI;

namespace Ninito.UsualSuspects.FlexibleLayout
{
    /// <summary>
    ///     A flexible layout capable of imitating grid, horizontal and vertical layout, with high customizability
    /// </summary>
    [ExecuteInEditMode]
    public class FlexibleLayoutGroup : LayoutGroup
    {
        #region Private Fields

        [Header("Layout Parameters")]
        [SerializeField]
        private FitType fitType;

        [SerializeField]
        private int rows;

        [SerializeField]
        private int columns;

        [Header("Cell Parameters")]
        [SerializeField]
        private Vector2 cellSize;

        [SerializeField]
        private Vector2 spacing;

        [SerializeField]
        private bool fitX;

        [SerializeField]
        private bool fitY;

        #endregion

        #region LayoutGroup Implementations

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            SetRowAndColumnCount();
            cellSize = GetCellSize();
            AdjustChildrenAlongAxis();
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ClampRowAndColumnValues();
        }

        #endregion

        #region Private Methods

        private void ClampRowAndColumnValues()
        {
            rows = Mathf.Max(1, rows);
            columns = Mathf.Max(1, columns);
        }
        
        /// <summary>
        ///     Calculates and sets the row and column count, according to the fit type
        /// </summary>
        private void SetRowAndColumnCount()
        {
            int childCount = transform.childCount;
            float squareRoot = Mathf.Sqrt(childCount);

            if (IsFitTypeFixedColumnsOrRows()) return;

            fitX = true;
            fitY = true;

            rows = GetRowCount(childCount, squareRoot);
            columns = GetColumnCount(childCount, squareRoot);
        }

        private int GetColumnCount(int childCount, float squareRoot)
        {
            return fitType == FitType.Width || fitType == FitType.FixedRows
                ? Mathf.CeilToInt(childCount / (float) rows)
                : Mathf.CeilToInt(squareRoot);
        }

        private int GetRowCount(int childCount, float squareRoot)
        {
            return fitType == FitType.Height || fitType == FitType.FixedColumns
                ? Mathf.CeilToInt(childCount / (float) columns)
                : Mathf.CeilToInt(squareRoot);
        }

        private bool IsFitTypeFixedColumnsOrRows()
        {
            return fitType == FitType.FixedColumns || fitType == FitType.FixedRows;
        }

        /// <summary>
        ///     Gets the cell size for the elements of the grid
        /// </summary>
        /// <returns>The cell size for the element of the grid</returns>
        private Vector2 GetCellSize()
        {
            Rect rect = rectTransform.rect;
            float parentWidth = rect.width;
            float parentHeight = rect.height;

            float cellWidth = GetCellWidth(parentWidth);
            float cellHeight = GetCellHeight(parentHeight);

            return new Vector2 {x = fitX ? cellWidth : cellSize.x, y = fitY ? cellHeight : cellSize.y};
        }

        private float GetCellHeight(float parentHeight)
        {
            return parentHeight / rows - spacing.y / rows * (rows - 1) - padding.top / (float) rows -
                   padding.bottom / (float) rows;
        }

        private float GetCellWidth(float parentWidth)
        {
            return parentWidth / columns - spacing.x / columns * (columns - 1) -
                   padding.left / (float) columns - padding.right / (float) columns;
        }

        /// <summary>
        ///     Adjusts all children of the layout group along the rectTransform's axis
        /// </summary>
        private void AdjustChildrenAlongAxis()
        {
            if (columns == 0 || rows == 0) return;

            for (int index = 0; index < rectChildren.Count; index++)
            {
                int rowCount = index / columns;
                int columnCount = index % columns;

                RectTransform item = rectChildren[index];

                float xPosition = cellSize.x * columnCount + spacing.x * columnCount + padding.left;
                float yPosition = cellSize.y * rowCount + spacing.y * rowCount + padding.top;

                SetChildAlongAxis(item, 0, xPosition, cellSize.x);
                SetChildAlongAxis(item, 1, yPosition, cellSize.y);
            }
        }

        #endregion

        #region Private Enums

        /// <summary>
        ///     Types that control how content should be displayed in the FlexibleLayout
        /// </summary>
        private enum FitType
        {
            Uniform,
            Height,
            Width,
            FixedRows,
            FixedColumns
        }

        #endregion
    }
}