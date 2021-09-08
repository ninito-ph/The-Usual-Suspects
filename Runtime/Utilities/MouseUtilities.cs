using UnityEngine;

namespace Ninito.UsualSuspects.Utilities
{
    /// <summary>
    ///     A class containing utility functions related to the mouse
    /// </summary>
    public static class MouseUtilities
    {
        #region Public Methods

        /// <summary>
        ///     Gets mouse's world position for the current position and main camera
        /// </summary>
        /// <returns>The mouse's world position</returns>
        public static Vector3 GetMouseWorldPosition()
        {
            return GetMouseWorldPosition(screenPosition: Input.mousePosition, currentCamera: Camera.main);
        }

        /// <summary>
        ///     Gets the mouse's world position for a specific screen position and camera
        /// </summary>
        /// <param name="screenPosition">The screen's position of the mouse</param>
        /// <param name="currentCamera">The camera to get the mouse's position through</param>
        /// <returns>The mouse's world position</returns>
        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera currentCamera)
        {
            return currentCamera.ScreenToWorldPoint(position: screenPosition);
        }

        #endregion
    }
}