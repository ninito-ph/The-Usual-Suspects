using System;
using UnityEngine;

namespace ManyTools.UnityExtended
{
    /// <summary>
    ///     A field that is only displayed if a given condition is met
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class |
                             AttributeTargets.Struct | AttributeTargets.Struct)]
    public class ShowFieldIfAttribute : PropertyAttribute
    {
        #region Properties

        public string ConditionFieldName { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a ShowIfAttribute with a given condition
        /// </summary>
        /// <param name="conditionFieldName">The exact name of the field to use as a condition. Must be a bool</param>
        public ShowFieldIfAttribute(string conditionFieldName)
        {
            ConditionFieldName = conditionFieldName;
        }

        #endregion
    }
}