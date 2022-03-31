using System;
using Market;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform PlayerPositionController;
        public float NavMeshSpeed;
        private NavMeshAgent _navMeshAgent;

        private void OnEnable()
        {
            UIButtons.OnPlayerSpeedChange += UpdatePlayerSpeed;
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            NavMeshSpeed=PlayerMarketData.Instance.NavMeshSpeed;
            _navMeshAgent.speed = NavMeshSpeed;
        }

        private void LateUpdate()
        {
            if(PlayerPositionController)
                _navMeshAgent.SetDestination(PlayerPositionController.position);
        }

        public void UpdatePlayerSpeed()
        {
            NavMeshSpeed = PlayerMarketData.Instance.NavMeshSpeed;
            _navMeshAgent.speed = NavMeshSpeed;
        }

        private void OnDisable()
        {
            UIButtons.OnPlayerSpeedChange -= UpdatePlayerSpeed;
        }

    }
}
