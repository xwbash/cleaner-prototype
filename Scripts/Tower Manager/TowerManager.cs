using DG.Tweening;
using Market;
using UnityEngine;

namespace Tower_Manager
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_towerModels;
        [SerializeField] private MarketWorth m_marketWorth;
        [SerializeField] private ParticleSystem m_particleSystem;
        private GameObject _instantiatedTower;


        private void Start()
        {
            _instantiatedTower = m_towerModels[m_marketWorth.CurrentTowerIndex];
            InAnimationScale();
            UIButtons.OnTowerUpgrade += SpawnTowers;

        }
        private void SpawnTowers()
        {
            int currentIndex = m_marketWorth.CurrentTowerIndex;
            if (currentIndex >= m_towerModels.Length)
            {
                UIButtons.OnTowerUpgrade -= SpawnTowers;
            }
            else
            {
                OutAnimationScale();
                m_particleSystem.Play();
                _instantiatedTower = m_towerModels[currentIndex];
                InAnimationScale();
            }
            
        }

        private void OutAnimationScale()
        {
            _instantiatedTower.SetActive(false);
        }
        private void InAnimationScale()
        {
            _instantiatedTower.SetActive(true);
            _instantiatedTower.transform.localScale = new Vector3(0, 0, 0);
            _instantiatedTower.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 2f).SetEase(Ease.InOutQuint);
            
        }
    }
}