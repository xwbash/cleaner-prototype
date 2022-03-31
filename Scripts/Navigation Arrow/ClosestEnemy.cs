using System;
using System.Collections.Generic;
using Players.PlayerManager;
using TMPro;
using UnityEngine;

namespace Navigation_Arrow
{
    public class ClosestEnemy : MonoBehaviour
    {
        public Transform NearestObjectTransform = null;
        public TMP_Text TextEnemyValue;
        [SerializeField] private EnemyVisiblity m_enemyVisible;
        [SerializeField] private EnemyManagement m_enemyManagment;
        private List<Transform> _closestEnemyList = new List<Transform>();

        private void Start()
        {
            _closestEnemyList = m_enemyManagment.EnemyList;
        }

        void Update()
        {
            NearestObjectTransform = Nearest();
            AssignGameObject();
        }

        private void OnDisable() 
        {
            NearestObjectTransform = Nearest();
            AssignGameObject();
        }
        
        private Transform Nearest()
        {
            Transform tMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = gameObject.transform.position;
            foreach (var t in _closestEnemyList)
            {
                var dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }

            return tMin;
        }

        private void AssignGameObject()
        {
            if (NearestObjectTransform)
            {
                var nearestObjectCount = NearestObjectTransform.GetComponent<PlayerManager>();
                var totalPlayer = nearestObjectCount.TotalPlayer.Count.ToString();
                m_enemyVisible.EnemyToFollow = NearestObjectTransform.gameObject;
                TextEnemyValue.text = totalPlayer;
                gameObject.transform.LookAt(NearestObjectTransform);
            }
            
        }
    }
}