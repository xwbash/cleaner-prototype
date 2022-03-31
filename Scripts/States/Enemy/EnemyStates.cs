using Players.PlayerManager;

namespace States.Enemy
{
    public abstract class EnemyStates
    {
        public virtual void OnStateEnter(PlayerManager playerManage, PlayerRaider playerRaider)
        {
        }

        public virtual void OnStateExit()
        {
        
        }

    }
}
