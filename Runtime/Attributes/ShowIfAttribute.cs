using System;
using UnityEngine;

namespace Ninito.UsualSuspects.Attributes
{
    /// <summary>
    ///     A field that is only displayed if a given condition is met
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowIfAttribute : PropertyAttribute
    {
        #region Properties

        public string ConditionFieldName { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a ShowIfAttribute with a given condition
        /// </summary>
        /// <param name="conditionFieldName">The exact name of the field to use as a condition. Must be a bool</param>
        public ShowIfAttribute(string conditionFieldName)
        {
            ConditionFieldName = conditionFieldName;
        }

        #endregion
    }
}