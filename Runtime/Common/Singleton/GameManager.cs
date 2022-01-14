using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ninito.UsualSuspects.Common.Singleton
{
    /// <summary>
    ///     An extendable singleton for a Game Manager
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        #region Unity Callbacks

        // Start is called before the first frame update
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Loads a given scene
        /// </summary>
        /// <param name="sceneName">The build name of the scene</param>
        public virtual void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        ///     Quits the game application
        /// </summary>
        public virtual void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        #endregion
    }
}