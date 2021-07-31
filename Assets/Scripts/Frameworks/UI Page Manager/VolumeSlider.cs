using UnityEngine;
using UnityEngine.Audio;

namespace ManyTools.UnityExtended
{
    public class VolumeSlider : MonoBehaviour
    {
        #region Public Fields

        [SerializeField]
        private string groupName;

        [SerializeField]
        private AudioMixer mixer;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Changes the volume of an audio group on a logarithmic scale.
        /// </summary>
        /// <param name="sliderValue">The value of the slider</param>
        public void SetVolume(float sliderValue)
        {
            mixer.SetFloat(name: groupName, value: Mathf.Log10(f: sliderValue) * 20);
        }

        #endregion
    }
}