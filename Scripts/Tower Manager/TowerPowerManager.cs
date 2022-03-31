using System;
using System.Collections;
using Market;
using UnityEngine;

namespace Tower_Manager
{
    public class TowerPowerManager : MonoBehaviour
    {
        public int TowerPowerValue;
        public float TowerPowerBySecond;
        private float _minimumTowerSecond = 2f;
        private void OnEnable()
        {
           UIButtons.OnTowerUpgrade += MultiplyTowerValue;
           UIButtons.OnTowerUpgrade += DecreaseTowerTime;
        }

        private void OnDisable()
        {
            UIButtons.OnTowerUpgrade -= MultiplyTowerValue;
            UIButtons.OnTowerUpgrade -= DecreaseTowerTime;
        }

        private void Start()
        {
            StartCoroutine(IncreaseMoneyWithTime(TowerPowerBySecond));
        }
        public void DecreaseTowerTime()
        {
            TowerPowerBySecond -= 0.1f;
            Mathf.Clamp(TowerPowerBySecond, _minimumTowerSecond, TowerPowerBySecond);
        }
        public void MultiplyTowerValue()
        {
            TowerPowerValue *= 3;
        }
        private IEnumerator IncreaseMoneyWithTime(float second)
        {
            while (true)
            {
                yield return new WaitForSeconds(second);
                PlayerMarketData.Instance.AddGold(TowerPowerValue);
            }
        }
    }
}
