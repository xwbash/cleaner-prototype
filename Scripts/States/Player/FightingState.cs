
using Game_Manager;
using Players.PlayerManager;

namespace States.Player
{
    public  class FightingState : States
    {
        
        private PlayerManager _playerManager;
        public override void OnStateEnter(PlayerManager playerManager,GameManager gameManager)
        {
            _playerManager = playerManager;
            _playerManager.StartFighting();
            

        }

        public override void OnStateExit()
        {
            _playerManager.IsFighting = false;
            _playerManager.StopFighting();
        }
    }
}