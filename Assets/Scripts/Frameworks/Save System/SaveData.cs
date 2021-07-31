using System;
using System.Collections.Generic;

namespace ManyTools.UnityExtended.SaveSystem
{
    /// <summary>
    ///     A class that contains all saved data
    /// </summary>
    [Serializable]
    public class SaveData
    {
        public Dictionary<string, object> Data = new Dictionary<string, object>();
    }
}