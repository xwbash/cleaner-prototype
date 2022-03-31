using AnimationMachine;
using Game_Manager;
using Market;
using UnityEngine;

namespace Interactor
{
    public class InteractMarket : MonoBehaviour, IInteractable
    {
        [SerializeField] private AnimatorManager m_animatorManager;
        [SerializeField] private GameManager m_gameManager;
        private PlayerMarketData _marketData;
        private void Start()
        {
            _marketData = PlayerMarketData.Instance;
        }

        public void Interact()
        {
            _marketData.GetMarketCanvas().SetActive(true);
            m_gameManager.SetActiveJoystick(false);
            StartCoroutine(m_animatorManager.MovementTreeValue(0));
        }
        public void UnInteract()
        {
            _marketData.GetMarketCanvas().SetActive(false);
            m_gameManager.SetActiveJoystick(true);
        }
    }
}