using System;
using UnityEngine;

namespace Ninito.UsualSuspects.Parallax
{
    /// <summary>
    ///     A class that controls and defines a Parallax Layer
    /// </summary>
    [Serializable]
    public class ParallaxLayer
    {
        #region Private Fields

        [SerializeField]
        [Tooltip(tooltip: "The layer's transform component")]
        private SpriteRenderer layerRenderer;

        [SerializeField]
        [Tooltip(
            tooltip: "The layer's parallax factor. It multiplies the movement of the layer relative to the camera. " +
                     "Objects at the focus should have a factor of 0, objects infinitely away should have a factor" +
                     " of 0, and objects at the same depth of the camera should have a factor of -1.")]
        private Vector2 parallaxFactor = new Vector2(x: 0f, y: 0f);

        [SerializeField]
        [Tooltip(tooltip: "Whether the layer should be repeated horizontally to allow for infinite scrolling")]
        private bool infiniteHorizontalScrolling = true;

        [SerializeField]
        [Tooltip(tooltip: "Whether the layer should be repeated vertically to allow for infinite scrolling")]
        private bool infiniteVerticalScrolling;

        private Vector2 textureUnitSize;

        #endregion

        #region Properties

        public GameObject LayerOriginal => layerRenderer.gameObject;
        public GameObject[] HorizontalClones { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets up layer clones as necessary
        /// </summary>
        public void Setup(Transform layerParent)
        {
            if (!infiniteHorizontalScrolling && !infiniteVerticalScrolling) return;

            // Set mode to tiled to allow for seamless infinite scrolling
            layerRenderer.drawMode = SpriteDrawMode.Tiled;

            textureUnitSize = GetTextureUnitSize();
            SetSpriteRendererSize();
        }

        /// <summary>
        ///     Moves the layer and all existing clones
        /// </summary>
        public void Move(Vector2 deltaMovement, Vector2 cameraPosition)
        {
            // Cache repeatedly accessed data
            Transform transform = layerRenderer.transform;
            Vector3 position = transform.position;

            // Translates layer based on parallax factor
            transform.Translate(translation: deltaMovement * parallaxFactor);

            if (infiniteHorizontalScrolling)
            {
                HandleTextureLoop(
                    cameraPositionInAxis: ref cameraPosition.x,
                    positionInAxis: ref position.x,
                    positionInOtherAxis: ref position.y,
                    textureUnitSizeInAxis: ref textureUnitSize.x,
                    offsetAddition: AddOffsetToPositionInX
                );
            }

            if (!infiniteVerticalScrolling) return;
            {
                HandleTextureLoop(
                    cameraPositionInAxis: ref cameraPosition.y,
                    positionInAxis: ref position.y,
                    positionInOtherAxis: ref position.x,
                    textureUnitSizeInAxis: ref textureUnitSize.y,
                    offsetAddition: AddOffsetToPositionInY
                );
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Sets the sprite render's
        /// </summary>
        private void SetSpriteRendererSize()
        {
            Vector2 size = layerRenderer.size;

            if (infiniteHorizontalScrolling)
            {
                size.x *= 3;
            }

            if (infiniteVerticalScrolling)
            {
                size.y *= 3;
            }

            layerRenderer.size = size;
        }

        /// <summary>
        ///     Gets the texture's X and Y size in world unites
        /// </summary>
        /// <returns>The texture's width and height in world units</returns>
        private Vector2 GetTextureUnitSize()
        {
            Sprite sprite = layerRenderer.sprite;
            Texture2D layerTexture = sprite.texture;
            Vector3 localScale = layerRenderer.transform.localScale;

            return new Vector2
            {
                x = layerTexture.width / sprite.pixelsPerUnit * localScale.x,
                y = layerTexture.height / sprite.pixelsPerUnit * localScale.y
            };
        }

        /// <summary>
        ///     Handles seamless loop logic for the layer's texture in a specified axis
        /// </summary>
        /// <param name="cameraPositionInAxis">The camera's position in the desired loop axis</param>
        /// <param name="positionInAxis">The position of the layer in the desired loop axis</param>
        /// <param name="positionInOtherAxis">The position of the layer in the opposite axis</param>
        /// <param name="textureUnitSizeInAxis">The layer's texture's size in units in the desired loop axis</param>
        /// <param name="offsetAddition">The offset addition operation</param>
        private void HandleTextureLoop(ref float cameraPositionInAxis, ref float positionInAxis,
            ref float positionInOtherAxis, ref float textureUnitSizeInAxis, Action<float, float, float> offsetAddition)
        {
            // If the texture hasn't fully looped, don't seamlessly move it yet
            if (!ShouldTextureLoop(
                cameraPositionInAxis: cameraPositionInAxis,
                positionInAxis: positionInAxis,
                textureLenghtInAxis: textureUnitSizeInAxis
            ))
            {
                return;
            }

            // Get how much the texture moved beyond a full loop
            float offset = (cameraPositionInAxis - positionInAxis) % textureUnitSizeInAxis;

            // Use the given offset addition to move loop the texture
            offsetAddition(arg1: offset, arg2: cameraPositionInAxis, arg3: positionInOtherAxis);
        }

        /// <summary>
        ///     An offset addition operation for the X axis
        /// </summary>
        /// <param name="offset">The offset to add</param>
        /// <param name="cameraPositionInX">The camera's position in the X axis</param>
        /// <param name="positionY">The layer's position in the Y axis</param>
        private void AddOffsetToPositionInX(float offset, float cameraPositionInX, float positionY)
        {
            layerRenderer.transform.position = new Vector3(x: cameraPositionInX + offset, y: positionY);
        }

        /// <summary>
        ///     An offset addition operation for the Y axis
        /// </summary>
        /// <param name="offset">The offset to add</param>
        /// <param name="cameraPositionInY">The camera's position in the Y axis</param>
        /// <param name="positionX">The layer's position in the X axis</param>
        private void AddOffsetToPositionInY(float offset, float cameraPositionInY, float positionX)
        {
            layerRenderer.transform.position = new Vector3(x: positionX, y: cameraPositionInY + offset);
        }

        /// <summary>
        ///     Calculates whether the texture should loop
        /// </summary>
        /// <param name="cameraPositionInAxis">The camera's position in the desired loop axis</param>
        /// <param name="positionInAxis">The layer's position in the desired loop axis</param>
        /// <param name="textureLenghtInAxis">The layer's texture's lenght in the desired loop axis</param>
        /// <returns></returns>
        private bool ShouldTextureLoop(float cameraPositionInAxis, float positionInAxis, float textureLenghtInAxis)
        {
            return Mathf.Abs(f: cameraPositionInAxis - positionInAxis) >= textureLenghtInAxis;
        }

        #endregion
    }
}