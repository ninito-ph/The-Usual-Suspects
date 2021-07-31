using System;
using UnityEngine;

namespace ManyTools.UnityExtended.Editor
{
    /// <summary>
    ///     A field attribute that only receives an interface of the given type
    /// </summary>
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        public Type InterfaceType { get; }

        public RequireInterfaceAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }
}