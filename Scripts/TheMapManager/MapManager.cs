using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheMapManager
{
    public class MapManager : MonoBehaviour
    {
        public delegate void OnPlayerOpensMap();
        public OnPlayerOpensMap OnPlayerOpenMap;
        public int CurrentMapIndex = 1;
        [SerializeField] private List<GameObject> m_mapGameObjects = new List<GameObject>();

        private void OnEnable()
        {
            OnPlayerOpenMap += HahaYes;
        }

        void Start()
        {
            int index = 0;
            foreach (var mapGameObject in m_mapGameObjects)
            {
                if (index > CurrentMapIndex)
                {
                    mapGameObject.SetActive(false);
                }
                else
                {
                    mapGameObject.SetActive(true);
                }
                index++;
            }
        }

        private void OnDisable()
        {
            OnPlayerOpenMap -= HahaYes;
        }
        [Button]
        private void OpenMap()
        {
            var CountSize = CurrentMapIndex + 1;
            if (!(m_mapGameObjects.Count <= CountSize))
            {
                OnPlayerOpenMap();
                CurrentMapIndex++;
                m_mapGameObjects[CurrentMapIndex].SetActive(true);
            }
        }

        private void HahaYes()
        {
            print("map is opened");
        }
        
    }
}
