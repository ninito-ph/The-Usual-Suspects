using UnityEngine;
using UnityEngine.SceneManagement;

namespace ManyTools.UnityExtended
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
            DontDestroyOnLoad(target: gameObject);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Loads a given scene
        /// </summary>
        /// <param name="sceneName">The build name of the scene</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName: sceneName);
        }

        /// <summary>
        ///     Quits the game application
        /// </summary>
        public void QuitApplication()
        {
            Application.Quit();
        }

        #endregion
    }
}