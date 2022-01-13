using Ninito.UsualSuspects;
using Ninito.UsualSuspects.Attributes;
using UnityEngine;

namespace Ninito.UnityExtended.WindowManager
{
    /// <summary>
    /// A script that controls basic functions relating to a window-based menu
    /// </summary>
    public sealed class WindowManager : MonoBehaviour
    {
        #region Private Fields

        [Header("Sound Effects")]
        [SerializeField]
        [RequireField]
        [Tooltip("The window open sound")]
        private AudioClip openSound;

        [SerializeField]
        [RequireField]
        [Tooltip("The window close sound")]
        private AudioClip closeSound;

        [Header("Dependencies")]
        [SerializeField]
        [RequireField]
        [Tooltip("The AudioSource used to play interface sounds.")]
        private AudioSource menuAudioSource;

        [SerializeField]
        private SerializedDictionary<string, GameObject> menuObjects = new SerializedDictionary<string, GameObject>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Switches to a given menu
        /// </summary>
        /// <param name="menuKey">The desired menu's key</param>
        public void SwitchToMenu(string menuKey)
        {
            // For each menu in the menus dictionary
            foreach (GameObject menu in menuObjects.Values)
            {
                menu.SetActive(menu == menuObjects[menuKey]);
            }

            PlayWindowSound(openSound);
        }

        /// <summary>
        /// Overlays a given menu
        /// </summary>
        /// <param name="desiredMenu">The menu to be overlayed</param>
        public void OverlayMenu(string desiredMenu)
        {
            // Sets the given menu active
            menuObjects[desiredMenu].SetActive(true);

            PlayWindowSound(openSound);
        }

        /// <summary>
        /// Disables a menu
        /// </summary>
        /// <param name="desiredMenu">The menu to be disabled</param>
        public void DisableMenu(string desiredMenu)
        {
            // Sets the given menu inactive
            menuObjects[desiredMenu].SetActive(false);

            PlayWindowSound(closeSound);
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Plays the desired sound
        /// </summary>
        private void PlayWindowSound(AudioClip sound)
        {
            menuAudioSource.clip = sound;
            menuAudioSource.Play();
        }

        #endregion
    }
}