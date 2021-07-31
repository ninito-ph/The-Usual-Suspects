using UnityEngine;
using UnityEngine.UI;

namespace ManyTools.UnityExtended.FlexibleLayout
{
    /// <summary>
    ///     A flexible layout capable of imitating grid, horizontal and vertical layout, with high customizability
    /// </summary>
    public class FlexibleLayout : LayoutGroup
    {
        #region Private Fields

        [SerializeField]
        private FitType fitType;

        [SerializeField]
        private int rows;

        [SerializeField]
        private int columns;

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

        #endregion

        #region Private Methods

        /// <summary>
        ///     Calculates and sets the row and column count, according to the fit type
        /// </summary>
        private void SetRowAndColumnCount()
        {
            int childCount = transform.childCount;
            float squareRoot = Mathf.Sqrt(f: childCount);

            if (fitType != FitType.Height && fitType != FitType.Width && fitType != FitType.Uniform) return;

            fitX = true;
            fitY = true;

            rows = fitType == FitType.Width || fitType == FitType.FixedColumns
                ? Mathf.CeilToInt(f: childCount / (float) columns)
                : Mathf.CeilToInt(f: squareRoot);

            columns = fitType == FitType.Height || fitType == FitType.FixedRows
                ? Mathf.CeilToInt(f: childCount / (float) rows)
                : Mathf.CeilToInt(f: squareRoot);
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

            float cellWidth = parentWidth / columns - spacing.x / columns * (columns - 1) -
                              padding.left / (float) columns - padding.right / (float) columns;

            float cellHeight = parentHeight / rows - spacing.y / rows * (rows - 1) - padding.top / (float) rows -
                               padding.bottom / (float) rows;

            return new Vector2 {x = fitX ? cellWidth : cellSize.x, y = fitY ? cellHeight : cellSize.y};
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

                RectTransform item = rectChildren[index: index];

                float xPosition = cellSize.x * columnCount + spacing.x * columnCount + padding.left;
                float yPosition = cellSize.y * rowCount + spacing.y * rowCount + padding.top;

                SetChildAlongAxis(rect: item, axis: 0, pos: xPosition, size: cellSize.x);
                SetChildAlongAxis(rect: item, axis: 1, pos: yPosition, size: cellSize.y);
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
            Width,
            Height,
            FixedRows,
            FixedColumns
        }

        #endregion
    }
}