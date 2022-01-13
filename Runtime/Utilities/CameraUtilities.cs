using UnityEngine;

namespace Ninito.TheUsualSuspects.Utilities
{
    /// <summary>
    /// A class that represents a single card in the game.
    /// </summary>
    public static class CameraUtilities
    {
        #region Private Fields

        private static Camera _mainCamera;

        #endregion

        #region Properties

        public static Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }
                
                return _mainCamera;
            }
        }

        #endregion
    }
}
