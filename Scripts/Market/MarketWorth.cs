using UnityEngine;

namespace Market
{
    public class MarketWorth : MonoBehaviour
    {
        public delegate void OnMarketOpen();
        public OnMarketOpen OnMarketOpened;

        public int ReloadSpeedWorth;
        public int SpeedPlayerWorth;
        public int TowerUpgradeWorth;
        public int CurrentTowerIndex;

  
        private void OnEnable()
        {
            OnMarketOpened();
        }
        
        public void MultiplyPlayerSpeedWorth(int value)
        {
            SpeedPlayerWorth *= value;
        }
        public void MultiplyReloadSpeedWorth(int value)
        {
            ReloadSpeedWorth *= value;
        }

        public void MultiplyTowerUpgradeWorth(int value)
        {
            TowerUpgradeWorth *= value;
        }

        public void AdditionTowerIndex(int value)
        {
            CurrentTowerIndex += value;
            
        }

        
    }
}