using UnityEngine;

namespace Ninito.UsualSuspects.CommonExtensions
{
    /// <summary>
    ///     An extension class for ScriptableObject
    /// </summary>
    public static class ScriptableObjectExtension
    {
        #region Public Methods

        /// <summary>
        ///     Creates and returns a clone of a given scriptable object
        /// </summary>
        /// <param name="scriptableObject">The ScriptableObject to clone</param>
        /// <typeparam name="T">The type of the ScriptableObject</typeparam>
        /// <returns>A clone of the given ScriptableObject</returns>
        public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
        {
            if (scriptableObject == null)
            {
                Debug.LogError(message: $"ScriptableObject was null. Returning default {typeof(T)} object.");
                return ScriptableObject.CreateInstance<T>();
            }

            T instance = Object.Instantiate(original: scriptableObject);
            return instance;
        }

        #endregion
    }
}