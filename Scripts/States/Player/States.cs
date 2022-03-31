using Game_Manager;
using Players.PlayerManager;

namespace States.Player
{
    public abstract class States
    {
        public virtual void OnStateEnter(PlayerManager playerManager, GameManager gameManager)
        {
        
        }

        public virtual void OnStateExit()
        {
        
        }

    }
}
