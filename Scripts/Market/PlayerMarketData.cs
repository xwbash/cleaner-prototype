using TMPro;
using UnityEngine;

namespace Market
{
    
    public class PlayerMarketData : Monosingleton<PlayerMarketData>
    {
        public float ReloadSpawnSpeed=5;
        public float Speed=2;
        public float NavMeshSpeed;

        [SerializeField] private GameObject m_marketCanvas;
        [SerializeField] private TMP_Text m_goldValueText;
        private int _goldValue;

        private void OnEnable()
        {
            UIButtons.OnReloadSpeedChange += GoldTextUpdate;
        }

        private void OnDisable()
        {
            UIButtons.OnPlayerSpeedChange -= IncreasePlayerSpeed;

        }
        public void GoldTextUpdate()
        {
            m_goldValueText.text = GetCurrentGold.ToString();
        }

        public void AddGold(int value)
        {
            _goldValue += value;
            GoldTextUpdate();
        }

        public void DecreaseGold(int value)
        {
            _goldValue -= value;
            GoldTextUpdate();
        }

        public GameObject GetMarketCanvas()
        {
            return m_marketCanvas;
        }

        public int SetCurrentGold
        {
            set => _goldValue = value;
        }
        public int GetCurrentGold => _goldValue;


        public void IncreasePlayerSpeed()
        {
            Speed += 0.1f;
        }

    }
    
}