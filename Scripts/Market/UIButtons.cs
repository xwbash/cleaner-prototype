using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Market
{
    public class UIButtons : MonoBehaviour
    {
        public delegate void PlayerSpeedDelegate();
        public static PlayerSpeedDelegate OnPlayerSpeedChange;
        
        public delegate void PlayerReloadSpeedDelegate();
        public static PlayerReloadSpeedDelegate OnReloadSpeedChange;
        
        public delegate void TowerUpgradeDelegate();
        public static TowerUpgradeDelegate OnTowerUpgrade;
        
        [SerializeField] private Button m_upgradeReloadSpeed, m_upgradePlayerSpeed, m_upgradeTower, m_closeMarket, m_retryScene;
        [SerializeField] private MarketWorth m_marketWorth;
        [SerializeField] private TMP_Text m_speedPlayerWorthText, m_towerUpgradeWorthText, m_reloadSpeedWorthText;
        [SerializeField] private PlayerMarketData m_marketData;
        [SerializeField] private GameManager m_gameManager;
        private void Awake()
        {
            m_upgradeReloadSpeed.onClick.AddListener(IncreaseReloadSpeed);
            m_upgradePlayerSpeed.onClick.AddListener(IncreaseSpeedPlayer);
            m_upgradeTower.onClick.AddListener(IncreaseTower);
            m_closeMarket.onClick.AddListener(CloseMarket);
            m_retryScene.onClick.AddListener(RetryCurrentScene);
        }
        public void RetryCurrentScene()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void CloseMarket()
        {
            m_gameManager.SetActiveJoystick(true);
            m_marketData.GetMarketCanvas().SetActive(false);
        }

        private void IncreaseTower()
        {
            if (m_marketWorth.TowerUpgradeWorth <= m_marketData.GetCurrentGold)
            {
                m_marketData.DecreaseGold(m_marketWorth.TowerUpgradeWorth);
                m_marketWorth.MultiplyTowerUpgradeWorth(2);
                m_marketWorth.AdditionTowerIndex(1);
                OnTowerUpgrade();
                MarketGoldUpdate();

            }
        }

        private void IncreaseSpeedPlayer()
        {
            if (m_marketWorth.SpeedPlayerWorth <= m_marketData.GetCurrentGold)
            {
                m_marketData.DecreaseGold(m_marketWorth.SpeedPlayerWorth);
                m_marketWorth.MultiplyPlayerSpeedWorth(2);
                m_marketData.NavMeshSpeed += 0.3f;
                OnPlayerSpeedChange();
                MarketGoldUpdate();

            }
        }

        private void IncreaseReloadSpeed()
        {
            if (m_marketWorth.ReloadSpeedWorth <= m_marketData.GetCurrentGold)
            {
                m_marketData.DecreaseGold(m_marketWorth.ReloadSpeedWorth);
                m_marketWorth.MultiplyReloadSpeedWorth(2);
                PlayerMarketData.Instance.ReloadSpawnSpeed -= 0.2f;
                OnReloadSpeedChange();
                MarketGoldUpdate();
            }
        }

        private void MarketGoldUpdate()
        {
            m_reloadSpeedWorthText.text = m_marketWorth.ReloadSpeedWorth.ToString();
            m_speedPlayerWorthText.text = m_marketWorth.SpeedPlayerWorth.ToString();
            m_towerUpgradeWorthText.text = m_marketWorth.TowerUpgradeWorth.ToString();
        }
    }
}