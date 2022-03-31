
using Game_Manager;
using Players.PlayerManager;

namespace States.Player
{
    public  class DyingState : States
    {
        public override void OnStateEnter(PlayerManager playerManager, GameManager gameManager)
        {
            gameManager.KillThePlayer();
            
        }

    }
}