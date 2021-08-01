#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Ninito.UsualSuspects.Editor
{
    /// <summary>
    ///     A property drawer for readonly fields in the inspector
    /// </summary>
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        private RequireInterfaceAttribute _requireInterfaceAttribute;
        private Color _cachedColor;

        #region PropertyDrawer Overrides

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            if (!IsPropertyReferenceTyped(property))
            {
                WarnPropertyIsNotReferenceTyped(position, label);
                return;
            }

            GetInterfaceFieldAttribute();

            ColorLabel();
            DrawField(position, property, label);
            RestorePreviousColor();
        }

        #endregion

        #region Private Methods
        
        private void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            property.objectReferenceValue =
                EditorGUI.ObjectField(position, GetInterfaceLabel(label), property.objectReferenceValue,
                    _requireInterfaceAttribute.InterfaceType, true);

            EditorGUI.EndProperty();
        }

        private static GUIContent GetInterfaceLabel(GUIContent label)
        {
            GUIContent interfaceLabel = label;
            interfaceLabel.text = $"{label.text} (Interface)";
            return interfaceLabel;
        }

        private static bool IsPropertyReferenceTyped(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.ObjectReference;
        }

        private static void WarnPropertyIsNotReferenceTyped(Rect position, GUIContent label)
        {
            Color previousPropertyColor = GUI.color;
            GUI.color = Color.red;
            EditorGUI.LabelField(position, label, new GUIContent("Property must be reference-typed!"));
            GUI.color = previousPropertyColor;
        }

        private void GetInterfaceFieldAttribute()
        {
            _requireInterfaceAttribute ??= attribute as RequireInterfaceAttribute;
        }

        private void ColorLabel()
        {
            _cachedColor = GUI.color;
            GUI.color = new Color(0.54f, 0.87f, 0.63f, 1f);
        }
        
        private void RestorePreviousColor()
        {
            GUI.color = _cachedColor;
        }

        #endregion
    }
}
#endif