using TMPro;
using UnityEngine;

namespace Ninito.UsualSuspects.Utilities
{
    /// <summary>
    ///     A class that contains various utility functions related to text
    /// </summary>
    public static class TextUtilities
    {
        #region Public Methods

        /// <summary>
        ///     Instantiates text on the world
        /// </summary>
        /// <param name="parent">The parent to instantiate the text under</param>
        /// <param name="localPosition">The local position to instantiate the text at</param>
        /// <param name="text">The text to fill the text component with</param>
        /// <param name="fontSize">The font size of the text</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="alignment">The alignment of the text</param>
        /// <returns>The instantiated text component</returns>
        public static TMP_Text InstantiateWorldText(Transform parent, Vector3 localPosition, string text = default,
            int fontSize = 24, Color textColor = default, TextAlignmentOptions alignment = default)
        {
            var textObject = new GameObject(name: "World Text", typeof(TextMeshPro));
            Transform transformCache = textObject.transform;

            transformCache.SetParent(parent: parent, worldPositionStays: false);
            transformCache.localPosition = localPosition;

            textObject.TryGetComponent(component: out TextMeshPro textComponent);

            textComponent.alignment = alignment;
            textComponent.text = text;
            textComponent.fontSize = fontSize;
            textComponent.color = textColor;

            return textComponent;
        }

        #endregion
    }
}