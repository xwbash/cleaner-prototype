using System.Collections;
using System.Collections.Generic;
using Players.PlayerManager;
using States.Enemy;
using UnityEngine;

namespace EnemySpawnManager
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PlayerManager m_mainPlayer;
        [SerializeField] private List<Vector3> m_spawnPoints = new List<Vector3>();
        [SerializeField] private EnemyManagement m_enemyManagement;
        private WaitForSeconds _waitForSeconds = new WaitForSeconds(10);
        public void SpawnTimer(PlayerManager gameObject)
        {
            var _spawnObject = gameObject;
            var _stateMachineEnemy = _spawnObject.GetComponent<StateMachineEnemy>();
            StartCoroutine(WaitAndSpawn(_spawnObject, _stateMachineEnemy));
        }

        private IEnumerator WaitAndSpawn(PlayerManager spawnObject, StateMachineEnemy stateMachineEnemy)
        {
            yield return _waitForSeconds;
            spawnObject.gameObject.SetActive(true);
            m_enemyManagement.EnemyList.Add(spawnObject.transform);
            stateMachineEnemy.SetCurrentStateEnemy(stateMachineEnemy.IdleStateEnemy);
            int spawnSize = Random.Range(3, m_mainPlayer.SpawnSize);
            spawnObject.SpawnSize = spawnSize;
            var playerColor = spawnObject.PlayerScriptable.PlayerColor;
            var playerObj = spawnObject.PlayerScriptable.PlayerPrefab;
            spawnObject.SpawnPlayer(spawnSize, playerColor, playerObj);
            spawnObject.transform.localPosition = PickRandomVector();
            spawnObject.GetComponent<PlayerRaider>().UpdateStartPoint();
        }

        private Vector3 PickRandomVector()
        {
             return m_spawnPoints[Random.Range(0, m_spawnPoints.Count)];
        }
        
    }
}
