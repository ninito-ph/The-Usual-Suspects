// using UnityEngine;
// using UnityEngine.UI;
//
// namespace ManyTools.UnityExtended.ProgressBar
// {
//     /// <summary>
//     /// A class that contains a simple progress bar
//     /// </summary>
//     [ExecuteInEditMode]
//     public class ProgressBar : MonoBehaviour
//     {
//         #region Private Fields
//
//         [Header(header: "Image References")]
//         [SerializeField]
//         private Image mask;
//
//         [SerializeField]
//         private Image fill;
//
//         [Header(header: "Other Configurations")]
//         [SerializeField]
//         private Color color;
//
//         #endregion
//
//         #region Properties
//         
//         public IMeasurableProgress MeasurableProgress { get; set; }
//
//         #endregion
//
//         #region Unity Callbacks
//
//         // Update is called once per frame
//         private void Update()
//         {
//             mask.fillAmount = GetCurrentFill();
//             fill.color = color;
//         }
//
//         #endregion
//
//         #region Private Methods
//
//         /// <summary>
//         /// Gets the current fill of the progress bar
//         /// </summary>
//         /// <returns>The current fill of the progress bar</returns>
//         private float GetCurrentFill()
//         {
//             float currentOffset = MeasurableProgress.CurrentProgress - MeasurableProgress.MinimumProgress;
//             float maximumOffset = MeasurableProgress.MaxProgress - MeasurableProgress.MinimumProgress;
//
//             return currentOffset / maximumOffset;
//         }
//
//         #endregion
//     }
// }