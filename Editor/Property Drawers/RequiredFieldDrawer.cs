using Ninito.UsualSuspects.Attributes;
using UnityEditor;
using UnityEngine;

namespace Ninito.UsualSuspects.Editor
{
    /// <summary>
    ///     A property drawer for special fields
    /// </summary>
    [CustomPropertyDrawer(typeof(RequireFieldAttribute))]
    public sealed class RequiredFieldDrawer : PropertyDrawer
    {
        #region Private Fields

        private RequireFieldAttribute _requireFieldAttribute;

        #endregion

        #region PropertyDrawer Overrides

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // If the attribute is null, cache it
            _requireFieldAttribute ??= attribute as RequireFieldAttribute;

            if (property.objectReferenceValue == null)
            {
                // Caches the current UI color
                Color cachedColor = GUI.color;

                // Draws field with the color
                // ReSharper disable once PossibleNullReferenceException
                GUI.color = _requireFieldAttribute.FieldColor;
                EditorGUI.PropertyField(position, property, label);

                // Restores the previous UI color
                GUI.color = cachedColor;
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        #endregion
    }
}
