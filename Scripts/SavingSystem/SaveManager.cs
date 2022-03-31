using Game_Manager;
using Market;
using Players.PlayerManager;
using Tower_Manager;
using UnityEngine;

namespace SavingSystem
{
    public class SaveManager: MonoBehaviour
    {
        public SaveData PlayerData;
        public GoldData GoldData;
        [SerializeReference] public MarketData MarketData;
        [SerializeField] private MarketWorth m_marketWorth;
        [SerializeField] private PlayerMarketData m_marketData;
        [SerializeField] private TowerPowerManager m_towerManager;

        private string _gold = "gold";
        private string _playerdata = "playerdata";
        private string _marketdata = "marketdata";
        
        private void OnEnable()
        {
            UIButtons.OnTowerUpgrade += CollectMarketToData;
            UIButtons.OnPlayerSpeedChange += CollectMarketToData;
            UIButtons.OnReloadSpeedChange += CollectMarketToData;
            UIButtons.OnReloadSpeedChange += CollectGoldToData;
            UIButtons.OnTowerUpgrade += CollectGoldToData;
            UIButtons.OnPlayerSpeedChange += CollectGoldToData;
        }

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(_playerdata))
            {
                var playerJson = JsonUtility.ToJson(PlayerData);
                PlayerPrefs.SetString(_playerdata, playerJson);
                var goldJson = JsonUtility.ToJson(GoldData);
                PlayerPrefs.SetString(_gold, goldJson);
                var marketJson = JsonUtility.ToJson(MarketData);
                PlayerPrefs.SetString(_marketdata, marketJson);
                
            }
            m_marketData.GoldTextUpdate();
            Load();
            SaveAll();
        }

        private void OnDisable()
        {
            UIButtons.OnTowerUpgrade -= CollectMarketToData;
            UIButtons.OnPlayerSpeedChange -= CollectMarketToData;
            UIButtons.OnReloadSpeedChange -= CollectMarketToData;
            UIButtons.OnReloadSpeedChange -= CollectGoldToData;
            UIButtons.OnTowerUpgrade -= CollectGoldToData;
            UIButtons.OnPlayerSpeedChange -= CollectGoldToData;
        }

        public void SaveAll()
        {
            CollectGoldToData();
            CollectMarketToData();
            CollectPlayerToData();
        }

        public void Load()
        {
            var playerJson = PlayerPrefs.GetString(_playerdata);
            PlayerData = JsonUtility.FromJson<SaveData>(playerJson);
            var goldData = PlayerPrefs.GetString(_gold);
            GoldData = JsonUtility.FromJson<GoldData>(goldData);
            var marketJson = PlayerPrefs.GetString(_marketdata);
            MarketData = JsonUtility.FromJson<MarketData>(marketJson);
            CollectDataToVariables();
        }
        public void CollectGoldToData()
        {
            GoldData.GoldValue = m_marketData.GetCurrentGold;
            var json = JsonUtility.ToJson(GoldData);
            PlayerPrefs.SetString(_gold, json);
        }

        public void CollectPlayerToData()
        {
            GetSpawnSizes();
            FirstTimeAssignPositions();
            for (int i = 0; i < PlayerData.GameObjects.Count; i++)
            {
                PlayerData.Transforms[i] = PlayerData.GameObjects[i].transform.localPosition;
            }
            var json = JsonUtility.ToJson(PlayerData);
            PlayerPrefs.SetString(_playerdata, json);
        }
        public void CollectMarketToData()
        {
            MarketData.PlayerSpeedWorth = m_marketWorth.SpeedPlayerWorth;
            MarketData.ReloadSpeedWorth = m_marketWorth.ReloadSpeedWorth;
            MarketData.TowerUpgradeWorth = m_marketWorth.TowerUpgradeWorth;
            MarketData.CurrentTowerIndex = m_marketWorth.CurrentTowerIndex;
            MarketData.TowerGoldPower = m_towerManager.TowerPowerValue;
            MarketData.TowerGoldSecond = m_towerManager.TowerPowerBySecond;
            MarketData.PlayerSpeed = m_marketData.Speed;
            MarketData.SpawnReloadSpeed = m_marketData.ReloadSpawnSpeed;
            MarketData.NavMeshMovementSpeed = m_marketData.NavMeshSpeed;
            
            var marketData = JsonUtility.ToJson(MarketData);
            PlayerPrefs.SetString(_marketdata, marketData);
        }
        private void FirstTimeAssignPositions()
        {
            if (PlayerData.Transforms.Count != PlayerData.GameObjects.Count)
            {
                for (int i = 0; i < PlayerData.GameObjects.Count; i++)
                {
                    PlayerData.Transforms.Add(PlayerData.GameObjects[i].transform.localPosition);
                }
            }
        }

        public void ReIndexObjects()
        {
            for (int i = 0; i < PlayerData.GameObjects.Count; i++)
            {
                PlayerData.GameObjects[i].GetComponent<PlayerManager>().MyIndex = i;
            }
        }

        private void AssignPositions()
        {
            for (int i = 0; i < PlayerData.GameObjects.Count; i++)
            {
                PlayerData.GameObjects[i].transform.localPosition = PlayerData.Transforms[i];
            }
        }

        private void CollectDataToVariables()
        {
            m_marketWorth.SpeedPlayerWorth = MarketData.PlayerSpeedWorth; // Player hizi worth u
            m_marketWorth.ReloadSpeedWorth=  MarketData.ReloadSpeedWorth; // Player spawn reload hizi worth u
            m_marketWorth.TowerUpgradeWorth= MarketData.TowerUpgradeWorth; // Tower upgrade worthhu 
            m_marketWorth.CurrentTowerIndex= MarketData.CurrentTowerIndex; // Tower Indexi
            m_towerManager.TowerPowerValue=MarketData.TowerGoldPower; // Towerin verdigi para
            m_towerManager.TowerPowerBySecond = MarketData.TowerGoldSecond; //Towerin saniye basi suresi
            m_marketData.Speed = MarketData.PlayerSpeed; //  Player speed
            m_marketData.SetCurrentGold = GoldData.GoldValue; // Gold u
            m_marketData.ReloadSpawnSpeed = MarketData.SpawnReloadSpeed; // Spawn reload
            m_marketData.NavMeshSpeed = MarketData.NavMeshMovementSpeed; // Navmesh hizi

            FirstTimeAssignPositions();
            AssignPositions();
            AssignSpawnSizes();
            ReIndexObjects();
            foreach (var gameObjects in PlayerData.GameObjects)
            {
                var playerManager = gameObjects.GetComponent<PlayerManager>();
                playerManager.SpawnSize = PlayerData.SpawnSizes[playerManager.MyIndex];
            }
        }

        private void AssignSpawnSizes()
        {
            for (int i = 0; i < PlayerData.GameObjects.Count; i++)
            {
                PlayerData.GameObjects[i].GetComponent<PlayerManager>().SpawnSize=PlayerData.SpawnSizes[i];
            }
        }

        private void GetSpawnSizes()
        {
            for (int i = 0; i < PlayerData.GameObjects.Count; i++)
            {
                PlayerData.SpawnSizes[i]=PlayerData.GameObjects[i].GetComponent<PlayerManager>().SpawnSize;
            }
        }
    }
}