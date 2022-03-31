using System.Collections.Generic;
using UnityEngine;

namespace SavingSystem
{
    [System.Serializable]
    public class SaveData
    {

        #region Character Position

        public List<Vector3> Transforms = new List<Vector3>();
        public List<GameObject> GameObjects = new List<GameObject>();
        public List<int> SpawnSizes = new List<int>();

        #endregion
    }
}