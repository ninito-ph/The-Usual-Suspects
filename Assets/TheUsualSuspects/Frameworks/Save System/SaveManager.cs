using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ninito.UsualSuspects.SaveSystem
{
    /// <summary>
    ///     A class that handles collecting and saving all existent saveables in a scene
    /// </summary>
    public class SaveManager : Singleton<SaveManager>
    {
        #region Private Fields

        public static string SaveName { get; set; }
        public static string SaveFolderPath { get; private set; }
        public static string SaveExtension { get; private set; }

        public static string FinalSavePath => SaveFolderPath + SaveName + SaveExtension;

        #endregion

        #region Unity Callbacks

        protected override void Awake()
        {
            base.Awake();
            SaveFolderPath = $"{Application.persistentDataPath}/saves";
            SaveExtension = ".save";
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Saves current data
        /// </summary>
        public void Save()
        {
            StartCoroutine(routine: SaveAll());
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Saves all loaded ISaveables, in all loaded scenes, irrespective of active state
        /// </summary>
        private IEnumerator SaveAll()
        {
            var endOfFrame = new WaitForEndOfFrame();
            var saveData = new SaveData();

            foreach (ISaveable state in GetAllSaveables())
            {
                if (!state.ShouldSave) continue;

                yield return endOfFrame;

                state.BeforeSave();
                saveData.Data.Add(key: state.UID, value: state);

                yield return endOfFrame;
            }

            Serializer.Write(saveName: SaveName, data: saveData);
        }

        /// <summary>
        ///     Gets all MonoBehaviors that implement ISaveable, in all loaded scenes, in all gameObjects, including
        ///     inactive ones.
        /// </summary>
        /// <returns>A list with all loaded ISaveables</returns>
        private IEnumerable<ISaveable> GetAllSaveables()
        {
            var saveables = new List<ISaveable>();

            for (int index = 0, upper = SceneManager.sceneCount; index < upper; index++)
            {
                var rootObject = SceneManager.GetSceneAt(index: index).GetRootGameObjects();

                foreach (GameObject child in rootObject)
                {
                    saveables.AddRange(collection: GetComponentsInChildren<ISaveable>(includeInactive: true));
                }
            }

            return saveables;
        }

        #endregion
    }
}