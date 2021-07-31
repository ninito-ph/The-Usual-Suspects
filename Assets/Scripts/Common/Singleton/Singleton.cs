using UnityEngine;

namespace ManyTools.UnityExtended
{
	/// <summary>
	///     An extendable base for all singletons
	/// </summary>
	/// <typeparam name="T">The type T inheritor of SingletonBase</typeparam>
	public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        #region Private Fields

        [Header(header: "Singleton Parameters")]
        [SerializeField]
        [Tooltip(tooltip: "Whether the singleton should survive scene changes")]
        private bool dontDestroyOnLoad;

        // The global instance reference of the singleton

        #endregion

        #region Properties

        public static T Instance { get; private set; }

        // An accessor to check whether the singleton has been initialized
        public static bool IsInitialized => Instance != null;

        #endregion

        #region Unity Callbacks

        // Overrideable check if another instance exists on awake
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError(message: $"A second instance of singleton {(T) this} has been initialized!");
            }
            else
            {
                // Set the instance to this class (typecasted to the given type)
                Instance = (T) this;
            }

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(target: gameObject);
            }
        }

        // An overrideable clear instance action
        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        #endregion
    }
}