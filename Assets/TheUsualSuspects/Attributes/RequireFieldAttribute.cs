using System;
using UnityEngine;

namespace Ninito.UsualSuspects.Attributes
{
    /// <summary>
    ///     A field attribute that gives it a given color, should it be null
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RequireFieldAttribute : PropertyAttribute
    {
        #region Properties

        public Color FieldColor { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new special field attribute with a custom color
        /// </summary>
        /// <param name="fieldColor">The color of the field</param>
        public RequireFieldAttribute(Color fieldColor)
        {
            this.FieldColor = fieldColor;
        }

        /// <summary>
        ///     Creates a new special field attribute through a number of predefined contexts
        /// </summary>
        /// <param name="fieldType">The type of the field</param>
        /// <exception cref="ArgumentOutOfRangeException">No such field type exists</exception>
        public RequireFieldAttribute(RequiredFieldType fieldType = RequiredFieldType.Error)
        {
            switch (fieldType)
            {
                case RequiredFieldType.Error:
                    FieldColor = Color.red;
                    break;
                case RequiredFieldType.Warning:
                    FieldColor = Color.yellow;
                    break;
                case RequiredFieldType.Exception:
                    FieldColor = Color.magenta;
                    break;
                default:
                    Debug.LogError(message: "Given field type does not exist");
                    break;
            }
        }

        #endregion

        #region Required Field Type Enum

        /// <summary>
        ///     An enum containing common field types
        /// </summary>
        public enum RequiredFieldType
        {
            Exception,
            Error,
            Warning
        }

        #endregion
    }
}