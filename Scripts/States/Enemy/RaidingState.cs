using Players.PlayerManager;

namespace States.Enemy
{
    public class RaidingState : EnemyStates
    {
        private PlayerRaider _playerRaider;
        public override void OnStateEnter(PlayerManager playerManager, PlayerRaider playerRaider)
        {
            _playerRaider=playerRaider;
            _playerRaider.IsRaiding = true;
        }

        public override void OnStateExit()
        {
            _playerRaider.IsRaiding = false;
        }
    }
}