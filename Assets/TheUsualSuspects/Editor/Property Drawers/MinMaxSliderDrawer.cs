using System;
using Ninito.TheUsualSuspects.Attributes;
using UnityEditor;
using UnityEngine;

namespace Ninito.UsualSuspects.Editor
{
    /// <summary>
    ///     A property drawer for fields labeled with the Min Max Slider attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderDrawer : PropertyDrawer
    {
        #region Private Fields

        private float _min;
        private float _max;
        private MinMaxSliderAttribute _minMaxAttribute;

        #endregion

        #region Unity Callbacks

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _minMaxAttribute ??= attribute as MinMaxSliderAttribute;
            SerializedPropertyType propertyType = property.propertyType;

            Rect controlRect = EditorGUI.PrefixLabel(position, label);
            RectPieces splitRect = PositionRectPieces(SplitRectIntoThree(controlRect));

            switch (propertyType)
            {
                case SerializedPropertyType.Vector2:
                    DrawFloatSliderOf(property, splitRect);
                    return;
                case SerializedPropertyType.Vector2Int:
                    DrawIntSliderOf(property, splitRect);
                    return;
                default:
                    WarnFieldTypeIsInvalid(position, label);
                    return;
            }
        }

        #endregion

        #region Private Methods

        private static void WarnFieldTypeIsInvalid(Rect position, GUIContent label)
        {
            Color previousPropertyColor = GUI.color;
            GUI.color = Color.red;
            EditorGUI.LabelField(position, label, new GUIContent("Field must be Vector2 or Vector2Int!"));
            GUI.color = previousPropertyColor;
        }

        /// <summary>
        ///     Splits a rect into three pieces
        /// </summary>
        /// <param name="rect">The rect to split into three</param>
        /// <returns>The rect split into three pieces</returns>
        private static RectPieces SplitRectIntoThree(Rect rect)
        {
            var rects = new Rect[3];
                
            for (int piece = 0; piece < rects.Length; piece++)
            {
                rects[piece] = new Rect(rect.position.x + piece * rect.width / rects.Length, rect.position.y,
                    rect.width / rects.Length, rect.height);
            }

            return new RectPieces(rects[0], rects[1], rects[2]);
        }

        /// <summary>
        ///     Adjusts the split pieces into the proper position for the slider's drawer
        /// </summary>
        /// <param name="pieces">The pieces to position</param>
        /// <returns>The positioned pieces</returns>
        private static RectPieces PositionRectPieces(RectPieces pieces)
        {
            int paddingSpace = (int)pieces.left.width - 50;
            const int spacing = 5;

            pieces.left.width -= paddingSpace + spacing;
            pieces.right.width -= paddingSpace + spacing;

            pieces.center.x -= paddingSpace;
            pieces.center.width += paddingSpace * 2;

            pieces.right.x += paddingSpace + spacing;

            return pieces;
        }

        /// <summary>
        ///     Clamps the min and max values of the sliders into the min and max values of the attribute
        /// </summary>
        private void ClampMinMaxValues()
        {
            if (_min < _minMaxAttribute.Min)
            {
                _max = _minMaxAttribute.Min;
            }

            if (_min > _minMaxAttribute.Max)
            {
                _max = _minMaxAttribute.Max;
            }
        }

        private void DrawFloatSliderOf(SerializedProperty property, RectPieces rectPieces)
        {
            EditorGUI.BeginChangeCheck();

            Vector2 vector = property.vector2Value;

            _min = vector.x;
            _max = vector.y;

            _min = EditorGUI.FloatField(rectPieces.left, Single.Parse(_min.ToString("F2")));
            _max = EditorGUI.FloatField(rectPieces.right, Single.Parse(_max.ToString("F2")));

            EditorGUI.MinMaxSlider(rectPieces.center, ref _min, ref _max, _minMaxAttribute.Min, _minMaxAttribute.Max);

            ClampMinMaxValues();

            vector = new Vector2(_min > _max ? _max : _min, _max);

            if (EditorGUI.EndChangeCheck())
            {
                property.vector2Value = vector;
            }
        }

        private void DrawIntSliderOf(SerializedProperty property, RectPieces rectPieces)
        {
            EditorGUI.BeginChangeCheck();

            Vector2Int vector = property.vector2IntValue;

            _min = vector.x;
            _max = vector.y;

            _min = EditorGUI.FloatField(rectPieces.left, _min);
            _max = EditorGUI.FloatField(rectPieces.right, _max);

            EditorGUI.MinMaxSlider(rectPieces.center, ref _min, ref _max, _minMaxAttribute.Min, _minMaxAttribute.Max);

            ClampMinMaxValues();

            vector = new Vector2Int(Mathf.FloorToInt(_min > _max ? _max : _min), Mathf.FloorToInt(_max));

            if (EditorGUI.EndChangeCheck())
            {
                property.vector2IntValue = vector;
            }
        }

        #endregion

        #region Private Classes

        /// <summary>
        ///     A struct that holds left, center and right pieces of a rect
        /// </summary>
        private struct RectPieces
        {
            public Rect left;
            public Rect center;
            public Rect right;

            public RectPieces(Rect left, Rect center, Rect right)
            {
                this.left = left;
                this.center = center;
                this.right = right;
            }
        }

        #endregion
    }
}