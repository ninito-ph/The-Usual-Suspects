using UnityEngine;
using UnityEngine.UI;

namespace ManyTools.UnityExtended
{
    [RequireComponent(requiredComponent: typeof(AudioSource))]
    public class UIPage : MonoBehaviour, IUIPage
    {
        #region Private Fields

        [Header(header: "UI Page Settings")]
        [SerializeField]
        private UIPageSettings settings;

        [SerializeField]
        private Image pageDisplay;

        private AudioSource _audioSource;

        #endregion

        #region Properties

        public GameObject PageGameObject => gameObject;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(component: out _audioSource);
        }

        #endregion

        #region IUIPage Implementation

        /// <summary>
        ///     Enters the page
        /// </summary>
        public void Enter()
        {
            gameObject.SetActive(value: true);
            PlayPageAudioClip(isEntry: true);
            SetDisplayState(active: true);

            if (settings.AffectMouse)
            {
                SetMouseState(locked: settings.LockMouseOnEnter, visible: !settings.HideMouseOnEnter);
            }
        }

        /// <summary>
        ///     Exits the page
        /// </summary>
        public void Exit()
        {
            gameObject.SetActive(value: false);
            PlayPageAudioClip(isEntry: false);
            SetDisplayState(active: false);

            if (settings.AffectMouse)
            {
                RestorePreviousMouseState();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Plays the page's entry or exit audio clip
        /// </summary>
        /// <param name="isEntry">Whether to play the entry clip (or exit clip for false)</param>
        private void PlayPageAudioClip(bool isEntry)
        {
            if (isEntry && settings.EnterClip != null)
            {
                _audioSource.PlayOneShot(clip: settings.EnterClip);
            }
            else if (settings.ExitClip != null)
            {
                _audioSource.PlayOneShot(clip: settings.ExitClip);
            }
        }

        /// <summary>
        ///     Sets the mouse state
        /// </summary>
        /// <param name="locked">Whether the mouse should be locked at the center of the screen</param>
        /// <param name="visible">Whether the mouse should be visible</param>
        private static void SetMouseState(bool locked, bool visible)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = visible;
        }

        /// <summary>
        ///     Restores the states of the mouse before the page entered
        /// </summary>
        private void RestorePreviousMouseState()
        {
            if (settings.LockMouseOnEnter)
            {
                SetMouseState(locked: false, visible: Cursor.visible);
            }

            if (settings.HideMouseOnEnter)
            {
                SetMouseState(locked: Cursor.lockState == CursorLockMode.Locked, visible: Cursor.visible);
            }
        }

        /// <summary>
        ///     Sets the state of the display for the page
        /// </summary>
        /// <param name="active">The state to set the display to</param>
        private void SetDisplayState(bool active)
        {
            pageDisplay.color = active ? settings.ActiveColor : settings.InactiveColor;
        }

        #endregion
    }
}