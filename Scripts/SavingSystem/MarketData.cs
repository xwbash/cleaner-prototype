namespace SavingSystem
{
    public class MarketData
    {
        #region PlayerMarketData
        
        public float PlayerSpeed;
        public float SpawnReloadSpeed;
        
        

        #endregion

        #region MarketWorth

        public int ReloadSpeedWorth;
        public int PlayerSpeedWorth;
        public int TowerUpgradeWorth;
        public int CurrentTowerIndex;

        #endregion

        #region Tower Manager

        public int TowerGoldPower;
        public float TowerGoldSecond;

        #endregion

        #region Player Movement Speed

        public float NavMeshMovementSpeed;

        #endregion
    }
}