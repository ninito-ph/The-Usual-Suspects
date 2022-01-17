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
        /// <param name="menuKey">The menu to be overlayed</param>
        public void OverlayMenu(string menuKey)
        {
            // Sets the given menu active
            menuObjects[menuKey].SetActive(true);

            PlayWindowSound(openSound);
        }

        /// <summary>
        /// Disables a menu
        /// </summary>
        /// <param name="menuKey">The menu to be disabled</param>
        public void DisableMenu(string menuKey)
        {
            // Sets the given menu inactive
            menuObjects[menuKey].SetActive(false);

            PlayWindowSound(closeSound);
        }

        /// <summary>
        /// Makes a menu inactive if it was active, and active if it was inactive
        /// </summary>
        /// <param name="menuKey">The menu to toggle</param>
        public void ToggleMenu(string menuKey)
        {
            menuObjects[menuKey].SetActive(!IsMenuActive(menuKey));
            PlayWindowSound(IsMenuActive(menuKey) ? openSound : closeSound);
        }

        /// <summary>
        /// Checks whether a given menu is active
        /// </summary>
        /// <param name="menuKey">The menu to check</param>
        /// <returns>Whether the menu is active</returns>
        public bool IsMenuActive(string menuKey)
        {
            return menuObjects[menuKey].activeSelf;
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