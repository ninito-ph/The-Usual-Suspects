#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Ninito.UsualSuspects.Editor
{
    /// <summary>
    ///     A property drawer for readonly fields in the inspector
    /// </summary>
    [CustomPropertyDrawer(type: typeof(ReadOnlyFieldAttribute))]
    public class ReadOnlyFieldDrawer : PropertyDrawer
    {
        #region PropertyDrawer Overrides

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            // Caches previous enabled state
            bool previousEnabledState = GUI.enabled;

            // Draws disabled field
            GUI.enabled = false;
            EditorGUI.PropertyField(position: position, property: property, label: label);

            // Restores previous state
            GUI.enabled = previousEnabledState;
        }

        #endregion
    }
}
#endif