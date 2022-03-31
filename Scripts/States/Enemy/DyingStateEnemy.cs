using Players.PlayerManager;
using States.Player;

namespace States.Enemy
{
    public  class DyingStateEnemy : EnemyStates
    {
        public override void OnStateEnter(PlayerManager playerManager, PlayerRaider playerRaider)
        {
            var stateMachine = StateMachine.Instance;
            stateMachine.SetStates(stateMachine.IdleState);
            playerManager.KillPlayer();
        }


    }
}