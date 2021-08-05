using Ninito.UsualSuspects.Attributes;
using UnityEditor;
using UnityEngine;

namespace Ninito.UsualSuspects.Editor
{
    /// <summary>
    ///     A property drawer for <see cref="ShowIfAttribute" />
    /// </summary>
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfFieldDrawer : PropertyDrawer
    {
        #region PropertyDrawer Overrides

        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var showIfAttribute = (ShowIfAttribute)attribute;

            if (ShouldShow(showIfAttribute, property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var showIfAttribute = (ShowIfAttribute)attribute;

            if (ShouldShow(showIfAttribute, property))
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }

            // Removes the space added before the property
            return -1f * EditorGUIUtility.standardVerticalSpacing;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets whether the field should be shown in the inspector
        /// </summary>
        /// <param name="showIfAttribute">The attribute to check for</param>
        /// <param name="property">The serialized property the attribute is being used on</param>
        /// <returns>Whether the field should be shown in the inspector</returns>
        private static bool ShouldShow(ShowIfAttribute showIfAttribute, SerializedProperty property)
        {
            // Gets the path to the property named as the condition in the attribute
            string conditionPath = property.propertyPath.Replace(property.name,
                showIfAttribute.ConditionFieldName);

            SerializedProperty conditionValue = property.serializedObject.FindProperty(conditionPath);

            if (conditionValue != null) return conditionValue.boolValue;

            Debug.LogError("ShowIfFieldAttribute can only check for a bool condition!");
            return false;
        }

        #endregion
    }
}