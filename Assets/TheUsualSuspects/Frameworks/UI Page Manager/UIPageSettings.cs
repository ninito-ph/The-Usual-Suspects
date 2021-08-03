using UnityEngine;

namespace Ninito.UsualSuspects
{
    /// <summary>
    ///     A data container class that
    /// </summary>
    [CreateAssetMenu(fileName = CreateAssetMenus.UIPageSettingsFileName,
        menuName = CreateAssetMenus.UIPageSettingsMenuName, order = CreateAssetMenus.UIPageSettingsOrder)]
    public class UIPageSettings : ScriptableObject
    {
        #region Properties

        [field: Header("Audio Settings")]
        [field: SerializeField]
        public AudioClip EnterClip { get; private set; } = null;

        [field: SerializeField]
        public AudioClip ExitClip { get; private set; } = null;

        [field: SerializeField]
        public bool AffectMouse { get; private set; } = false;

        [field: SerializeField]
        public bool LockMouseOnEnter { get; private set; } = false;

        [field: SerializeField]
        public bool HideMouseOnEnter { get; private set; } = false;

        [field: Header("Page Display")]
        [field: SerializeField]
        public Color ActiveColor { get; private set; } = Color.white;

        [field: SerializeField]
        public Color InactiveColor { get; private set; } = Color.white;

        #endregion
    }
}