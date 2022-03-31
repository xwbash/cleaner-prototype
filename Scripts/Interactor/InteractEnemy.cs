using System;
using Players.PlayerManager;
using UnityEngine;
using States.Player;
using States.Enemy;

namespace Interactor
{
    public class InteractEnemy : MonoBehaviour, IInteractable
    {
        [SerializeField] private RaidManager m_raidManager;
        private StateMachineEnemy _enemyStateMachine;
        private PlayerManager _playerManager;
        
        private void Awake()
        {
            _enemyStateMachine =  GetComponent<StateMachineEnemy>();
            _playerManager = GetComponent<PlayerManager>();
        }

        public void Interact()
        {
            if (!(_playerManager.SpawnSize <= 1))
            {
                StateMachine.Instance.SetStates(StateMachine.Instance.FightingState);
                if (_enemyStateMachine.GetCurrentEnemyState == _enemyStateMachine.RaidingStateEnemy)
                {
                    _playerManager.StartFighting();
                    m_raidManager.IsCurrentlyRaiding = true;
                }
                else
                { 
                    m_raidManager.IsCurrentlyRaiding = false;
                    m_raidManager.StopRaids();
                    _enemyStateMachine.SetCurrentStateEnemy(_enemyStateMachine.FightingStateEnemy);
                }
            }
        }

        public void UnInteract()
        {
            if (!(_playerManager.SpawnSize <= 1))
            {
                _playerManager.IsFighting = false;
                _playerManager.StopFighting();
                StateMachine.Instance.SetStates(StateMachine.Instance.IdleState);
                if (_enemyStateMachine.GetCurrentEnemyState != _enemyStateMachine.RaidingStateEnemy)
                {
                    m_raidManager.IsCurrentlyRaiding = false;
                    _enemyStateMachine.SetCurrentStateEnemy(_enemyStateMachine.IdleStateEnemy);
                    m_raidManager.RaidPlayer();
                }
            }
            
        }
    }
}