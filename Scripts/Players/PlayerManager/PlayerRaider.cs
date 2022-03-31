using AnimationMachine;
using Market;
using UnityEngine;
using UnityEngine.AI;

namespace Players.PlayerManager
{
    public class PlayerRaider : MonoBehaviour
    {
        public bool IsRaiding;
        [SerializeField] private Transform m_playerGameObject;
        private AnimatorManager _animatorManager;
        private NavMeshAgent _navMeshAgent;

        private Vector3 _spawnPoint;
        private void OnEnable()
        {
            UIButtons.OnPlayerSpeedChange += OnSpeedChanges;
        }
        
        private void Start()
        {
            _spawnPoint=transform.position;
            _animatorManager = GetComponent<AnimatorManager>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        private void Update()
        {
            RaidPlayer();
        }

        private void OnDisable()
        {
            UIButtons.OnPlayerSpeedChange -= OnSpeedChanges;
        }

        public void UpdateStartPoint()
        {
            _spawnPoint = transform.position;
        }
        public void OnSpeedChanges()
        {
            _navMeshAgent.speed = PlayerMarketData.Instance.Speed;
        }

        private void AnimationCheck()
        {
            if (Vector3.Distance(transform.position, _spawnPoint) < 2)
            {
                _animatorManager.OnMovement(0);
            }
        }
        private void RaidPlayer()
        {
            if (!IsRaiding)
            {
                _navMeshAgent.SetDestination(_spawnPoint);
                AnimationCheck();
                return;
            }
            _animatorManager.OnMovement(0.5f);
            _navMeshAgent.SetDestination(m_playerGameObject.position);
        }

      



    }
}