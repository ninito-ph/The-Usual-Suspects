using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ninito.UsualSuspects.Parallax
{
    /// <summary>
    ///     A class that handles and controls parallax background movement
    /// </summary>
    public class ParallaxController : MonoBehaviour
    {
        #region Private Fields

        [Header(header: "Targets")]
        [SerializeField]
        [Tooltip(tooltip: "The camera to which the parallax is relative to")]
        private Camera relativeCamera;

        [SerializeField]
        [Tooltip(
            tooltip: "Whether the relative camera is using Cinemachine. If it is, all update logic must be handled" +
                     " in FixedUpdate instead of LateUpdate.")]
        private bool isCinemachineCamera;

        [SerializeField]
        [Tooltip(tooltip: "The different parallax layers")]
        private List<ParallaxLayer> layers = new List<ParallaxLayer>();

        private Vector3 lastCameraPosition;

        #endregion

        #region Properties

        private Vector3 DeltaMovement { get; set; }

        #endregion

        #region Unity Callbacks

        // Start is called before the first frame update
        private void Start()
        {
            for (int index = 0, upper = layers.Count; index < upper; index++)
            {
                layers[index: index].Setup(layerParent: transform);
            }
        }

        private void FixedUpdate()
        {
            if (!isCinemachineCamera) return;

            // Updates delta movement
            Vector3 position = relativeCamera.transform.position;
            DeltaMovement = position - lastCameraPosition;

            // Updates last position
            lastCameraPosition = position;

            // Moves parallax layers
            MoveLayers();
        }

        private void LateUpdate()
        {
            if (isCinemachineCamera) return;

            // Updates delta movement
            Vector3 position = relativeCamera.transform.position;
            DeltaMovement = position - lastCameraPosition;

            // Updates last position
            lastCameraPosition = position;

            // Moves parallax layers
            MoveLayers();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Moves all parallax layers
        /// </summary>
        private void MoveLayers()
        {
            for (int index = 0, upper = layers.Count; index < upper; index++)
            {
                layers[index: index].Move(deltaMovement: DeltaMovement, cameraPosition: lastCameraPosition);
            }
        }

        #endregion
    }
}