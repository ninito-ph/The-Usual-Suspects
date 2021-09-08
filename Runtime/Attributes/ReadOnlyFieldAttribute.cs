using System;
using UnityEngine;

namespace Ninito.UsualSuspects.Attributes
{
    /// <summary>
    ///     A field attribute that marks a property as non-editable in the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyFieldAttribute : PropertyAttribute
    {
    }
}