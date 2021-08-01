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

        [field: Header(header: "Audio Settings")]
        [field: SerializeField]
        public AudioClip EnterClip { get; } = null;

        [field: SerializeField]
        public AudioClip ExitClip { get; } = null;

        [field: SerializeField]
        public bool AffectMouse { get; } = false;

        [field: SerializeField]
        public bool LockMouseOnEnter { get; } = false;

        [field: SerializeField]
        public bool HideMouseOnEnter { get; } = false;

        [field: Header(header: "Page Display")]
        [field: SerializeField]
        public Color ActiveColor { get; } = Color.white;

        [field: SerializeField]
        public Color InactiveColor { get; } = Color.white;

        #endregion
    }
}