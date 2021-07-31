using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ManyTools.UnityExtended.SaveSystem.Surrogates;
using UnityEngine;

namespace ManyTools.UnityExtended.SaveSystem
{
    /// <summary>
    ///     A class that manages saving and loading objects
    /// </summary>
    public static class Serializer
    {
        #region Public Methods

        /// <summary>
        ///     Saves data to a binary file with the given save name
        /// </summary>
        /// <param name="saveName">The name of the file to save to</param>
        /// <param name="data">The data to save</param>
        /// <returns>Whether saving succeeded</returns>
        public static bool Write(string saveName, object data)
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            EnsureSaveFolderExists();

            using FileStream file = File.Create(path: SaveManager.FinalSavePath);

            formatter.Serialize(serializationStream: file, graph: data);
            file.Close();

            return true;
        }

        /// <summary>
        ///     Loads a serialized binary save file at the given save path
        /// </summary>
        /// <param name="savePath">The save path to read from</param>
        /// <returns>The deserialized save object</returns>
        public static object Read(string savePath)
        {
            if (!File.Exists(path: savePath))
            {
                Debug.LogError(message: $"No save file existed at '{savePath}' path!");
                return null;
            }

            BinaryFormatter formatter = GetBinaryFormatter();

            using FileStream file = File.Open(path: savePath, mode: FileMode.Open);

            try
            {
                object save = formatter.Deserialize(serializationStream: file);
                file.Close();
                return save;
            }
            catch
            {
                Debug.LogError(message: "Failed to load save file!");
                file.Close();
                return null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the Binary Formatter for saving the game, with the appropriate serialization surrogates
        /// </summary>
        /// <returns>A binary formatter with custom serialization surrogates</returns>
        private static BinaryFormatter GetBinaryFormatter()
        {
            var binaryFormatter = new BinaryFormatter();
            var selector = new SurrogateSelector();

            AddSurrogatesToSelector(selector: selector);

            binaryFormatter.SurrogateSelector = selector;

            return binaryFormatter;
        }

        /// <summary>
        ///     Creates a save folder if one doesn't exist already
        /// </summary>
        private static void EnsureSaveFolderExists()
        {
            if (!Directory.Exists(path: SaveManager.SaveFolderPath))
            {
                Directory.CreateDirectory(path: SaveManager.SaveFolderPath);
            }
        }

        /// <summary>
        ///     Adds Unity-specific serialization surrogates to a given surrogate selector
        /// </summary>
        /// <param name="selector">The selector to add the surrogates to</param>
        private static void AddSurrogatesToSelector(SurrogateSelector selector)
        {
            // Creates surrogates
            var colorSurrogate = new ColorSerializationSurrogate();
            var quaternionSurrogate = new QuaternionSerializationSurrogate();
            var vector2Surrogate = new Vector2SerializationSurrogate();
            var vector3Surrogate = new Vector3SerializationSurrogate();
            var vector4Surrogate = new Vector4SerializationSurrogate();

            // Introduces surrogates
            selector.AddSurrogate(type: typeof(Color), context: new StreamingContext(state: StreamingContextStates.All),
                surrogate: colorSurrogate);
            selector.AddSurrogate(type: typeof(Quaternion),
                context: new StreamingContext(state: StreamingContextStates.All),
                surrogate: quaternionSurrogate);
            selector.AddSurrogate(type: typeof(Vector2),
                context: new StreamingContext(state: StreamingContextStates.All),
                surrogate: vector2Surrogate);
            selector.AddSurrogate(type: typeof(Vector3),
                context: new StreamingContext(state: StreamingContextStates.All),
                surrogate: vector3Surrogate);
            selector.AddSurrogate(type: typeof(Vector4),
                context: new StreamingContext(state: StreamingContextStates.All),
                surrogate: vector4Surrogate);
        }

        #endregion
    }
}