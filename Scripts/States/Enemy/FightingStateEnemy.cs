using Players.PlayerManager;

namespace States.Enemy
{
    public  class FightingStateEnemy : EnemyStates
    {
        private PlayerManager _playerManager;

        public override void OnStateEnter(PlayerManager playerManager, PlayerRaider playerRaider)
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