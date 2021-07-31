#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ManyTools.UnityExtended
{
    /// <summary>
    ///     A property drawer for <see cref="ShowFieldIfAttribute" />
    /// </summary>
    [CustomPropertyDrawer(type: typeof(ShowFieldIfAttribute))]
    public class ShowIfFieldDrawer : PropertyDrawer
    {
        #region PropertyDrawer Overrides

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var showIfAttribute = (ShowFieldIfAttribute) attribute;

            if (ShouldShow(showFieldIfAttribute: showIfAttribute, property: property))
            {
                EditorGUI.PropertyField(position: position, property: property, label: label, includeChildren: true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var showIfAttribute = (ShowFieldIfAttribute) attribute;

            if (ShouldShow(showFieldIfAttribute: showIfAttribute, property: property))
            {
                return EditorGUI.GetPropertyHeight(property: property, label: label);
            }

            // Removes the space added before the property
            return -1f * EditorGUIUtility.standardVerticalSpacing;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets whether the field should be shown in the inspector
        /// </summary>
        /// <param name="showFieldIfAttribute">The attribute to check for</param>
        /// <param name="property">The serialized property the attribute is being used on</param>
        /// <returns>Whether the field should be shown in the inspector</returns>
        private static bool ShouldShow(ShowFieldIfAttribute showFieldIfAttribute, SerializedProperty property)
        {
            // Gets the path to the property named as the condition in the attribute
            string conditionPath = property.propertyPath.Replace(oldValue: property.name,
                newValue: showFieldIfAttribute.ConditionFieldName);

            SerializedProperty conditionValue = property.serializedObject.FindProperty(propertyPath: conditionPath);

            if (conditionValue != null) return conditionValue.boolValue;

            Debug.LogError(message: "ShowIfFieldAttribute can only check for a bool condition!");
            return false;
        }

        #endregion
    }
}
#endif