using System;
using Game_Manager;
using Players.PlayerManager;
using UnityEngine;

namespace States.Player
{
    public class StateMachine : Monosingleton<StateMachine>
    {
        [Header("States")]
        [SerializeReference] public DyingState DyingState;
        [SerializeReference] public FightingState FightingState;
        [SerializeReference] public IdleState IdleState;
        [SerializeField] private GameManager m_gameManager;
        [SerializeReference] private States m_CurrentState;
        
        private PlayerManager _playerManager;
        private void Start()
        {
            _playerManager = GetComponent<PlayerManager>();
        }

        public States CurrentState
        {
            get => m_CurrentState;
            private set => m_CurrentState = value;
        }
        public void SetStates(States states)
        {
            if (CurrentState != DyingState)
            {
                CurrentState.OnStateExit();
                CurrentState = states;
                CurrentState.OnStateEnter(_playerManager, m_gameManager);
            }
        }


    }
}