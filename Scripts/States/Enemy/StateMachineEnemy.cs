using Players.PlayerManager;
using UnityEngine;

namespace States.Enemy
{
    public class StateMachineEnemy : MonoBehaviour
    {
        [SerializeReference] public IdleStateEnemy IdleStateEnemy;
        [SerializeReference] public FightingStateEnemy FightingStateEnemy;
        [SerializeReference] public RaidingState RaidingStateEnemy;
        [SerializeReference] public DyingStateEnemy DyingStateEnemy;
        [SerializeReference] private EnemyStates m_currentStateEnemy;
        private PlayerRaider _playerRaider;
        private PlayerManager _playerManager;
        private void Awake()
        {
            m_currentStateEnemy = IdleStateEnemy;
            _playerManager=GetComponent<PlayerManager>();
            _playerRaider = GetComponent<PlayerRaider>();
        }

        public EnemyStates GetCurrentEnemyState => m_currentStateEnemy;

        public void SetCurrentStateEnemy(EnemyStates states)
        {
                m_currentStateEnemy.OnStateExit();
                m_currentStateEnemy = states;
                m_currentStateEnemy.OnStateEnter(_playerManager, _playerRaider);
        }
    
    }
}