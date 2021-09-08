using System;
using System.Collections;

namespace Ninito.UsualSuspects.DataGrid
{
    /// <summary>
    ///     An enumerator for a grid
    /// </summary>
    public class DataGridEnumerator<T> : IEnumerator
    {
        #region Private Fields

        private readonly T[,] _grid;
        private int _positionX = -1;
        private int _positionY = -1;

        #endregion

        #region Properties

        public T Current
        {
            get
            {
                try
                {
                    return _grid[_positionX, _positionX];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        #endregion

        #region IEnumerator Implementation

        public DataGridEnumerator(T[,] grid)
        {
            _grid = grid;
        }

        public bool MoveNext()
        {
            bool canMoveOnX = _positionX < _grid.GetLength(dimension: 0);
            bool canMoveOnY = _positionY < _grid.GetLength(dimension: 1);

            if (canMoveOnX)
            {
                _positionX++;
            }
            else if (canMoveOnY)
            {
                _positionY++;
            }

            return canMoveOnX || canMoveOnY;
        }

        public void Reset()
        {
            _positionX = -1;
            _positionY = -1;
        }

        object IEnumerator.Current => Current;

        #endregion
    }
}