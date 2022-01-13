using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// A class that manages a Volume Slider
    /// </summary>
    public sealed class VolumeSlider : MonoBehaviour
    {
        #region Private Fields

        [Header("Audio")]
        [SerializeField]
        private string _audioGroupName;

        [SerializeField]
        private AudioMixer _audioMixer;

        [Header("UI")]
        [SerializeField]
        private Slider _slider;
        
        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            _slider.value = PlayerPrefs.GetFloat(_audioGroupName + "Volume", 1f);
            SetVolume(_slider.value);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(_audioGroupName + "Volume", _slider.value);
        }

        private void Reset()
        {
            TryGetComponent(out _slider);
            _slider.minValue = 0.0001f;
            _slider.maxValue = 1f;
            _slider.value = _slider.maxValue;
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Set's the volume of the audio group, taking into account logarithmic falloff
        /// </summary>
        /// <param name="volume">The volume in normalized percentage to set to</param>
        [UsedImplicitly]
        public void SetVolume(float volume)
        {
            _audioMixer.SetFloat(_audioGroupName, Mathf.Log10(volume) * 20);
        }

        #endregion
    }
}
