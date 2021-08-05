using UnityEngine;

namespace Ninito.UsualSuspects.AudioCue
{
    /// <summary>
    ///     A collection of audio clips with configurable settings
    /// </summary>
    [CreateAssetMenu(order = CreateAssetMenus.AudioCueOrder, fileName = CreateAssetMenus.AudioCueFileName,
        menuName = CreateAssetMenus.AudioCueMenuName)]
    public class AudioCue : ScriptableObject
    {
        #region Private Fields

        [Header("Audio Cue Parameters")]
        [SerializeField]
        private AudioClip[] clips;

        [Header("Pitch"), SerializeField]
        private float basePitch;

        [SerializeField]
        private float pitchDeviation;

        #endregion

        #region Public Methods

        public (AudioClip, float) GetClipWithPitch()
        {
            return (GetClip(), GetPitch());
        }

        public AudioClip GetClip()
        {
            return clips[Random.Range(0, clips.Length)];
        }

        public float GetPitch()
        {
            return basePitch + Random.Range(pitchDeviation * -1, pitchDeviation);
        }

        #endregion
    }
}