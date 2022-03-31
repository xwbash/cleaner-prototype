using System.Collections;
using System.Collections.Generic;
using States.Enemy;
using UnityEngine;

namespace Players.PlayerManager
{
    public class RaidManager : MonoBehaviour
    {
        public bool IsCurrentlyRaiding = false;
        [SerializeField] private EnemyManagement m_enemyManagement;
        private Transform _objectToRaid;
        private float _minRaidingSec=70f, _maxRaidingSec=120f;
        private List<Transform> _transformsEnemy = new List<Transform>(); 
        private void Start()
        {
            _transformsEnemy = m_enemyManagement.EnemyList;
            RaidPlayer();
        }
        public void RaidPlayer()
        {
            if (!IsCurrentlyRaiding)
            {
                StartCoroutine(WaitAndRaid());
            }
        }


        public void StopRaids()
        {
            if (_objectToRaid) // Adamin icindeyken oyundan cikilip girilirse null donmekte cunku basta fight olmus oluyor ve null donuyor.
            {
                var stateMachineEnemy = _objectToRaid.GetComponent<StateMachineEnemy>();
                stateMachineEnemy.SetCurrentStateEnemy(stateMachineEnemy.IdleStateEnemy);
            }
        }
        private IEnumerator WaitAndRaid()
        {
            yield return new WaitForSeconds(Random.Range(_minRaidingSec, _maxRaidingSec));
            if(_transformsEnemy.Count == 0)
                yield break;
            PickARandomObject();
            var stateMachine = _objectToRaid.GetComponent<StateMachineEnemy>();
            if (stateMachine.GetCurrentEnemyState == stateMachine.IdleStateEnemy)
            {
                stateMachine.SetCurrentStateEnemy(stateMachine.RaidingStateEnemy);
            }
        }
        private void PickARandomObject()
        {
            var randomValue = Random.Range(0, _transformsEnemy.Count);
            _objectToRaid=_transformsEnemy[randomValue];
        }
    }
}