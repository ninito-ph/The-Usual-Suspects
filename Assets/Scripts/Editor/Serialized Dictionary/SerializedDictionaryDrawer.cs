#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ManyTools.UnityExtended.Editor
{
    /// <summary>
    ///     A property drawer for a serialized dictionary
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryDrawer : PropertyDrawer
    {
        #region Private Fields

        private static readonly float LineHeight = EditorGUIUtility.singleLineHeight;
        private static readonly float VerticalSpace = EditorGUIUtility.standardVerticalSpacing;
        private const float WarningBoxHeight = 1.5f;

        #endregion

        #region PropertyDrawer Implementation

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draws a list with the KeyValueClass
            SerializedProperty keyValueList = property.FindPropertyRelative("keyValueList");
            EditorGUI.PropertyField(position, keyValueList, label, true);

            // Check if there are duplicate keys
            bool keyCollision = property.FindPropertyRelative("keyCollision").boolValue;
            // If there are, display warning
            if (!keyCollision) return;
            DisplayKeyCollision(position, keyValueList);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = 0f;

            // Gets height of the KeyValuePair class
            SerializedProperty listProperty = property.FindPropertyRelative("keyValueList");
            totalHeight += EditorGUI.GetPropertyHeight(listProperty, true);

            // Gets height of key collision warning
            bool keyCollision = property.FindPropertyRelative("keyCollision").boolValue;
            if (!keyCollision) return totalHeight;

            totalHeight += WarningBoxHeight * LineHeight;

            if (!listProperty.isExpanded)
            {
                totalHeight += VerticalSpace;
            }

            return totalHeight;
        }

        #endregion

        #region Private Methods

        private static void DisplayKeyCollision(Rect position, SerializedProperty keyValueList)
        {
            // Descends Y position to draw warning
            position.y += EditorGUI.GetPropertyHeight(keyValueList, true);

            // If the list is not expanded
            if (!keyValueList.isExpanded)
            {
                position.y += VerticalSpace;
            }

            // Updates warning box rect
            position.height = LineHeight * WarningBoxHeight;
            position = EditorGUI.IndentedRect(position);

            // Draws warning box
            EditorGUI.HelpBox(position, "Duplicate keys will be ignored!",
                MessageType.Warning);
        }

        #endregion
    }
}
#endif