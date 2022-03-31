using Players.PlayerManager;
using UnityEngine;

namespace Interactor
{
    public class InteractSpawner : MonoBehaviour, IInteractable
    {
        [SerializeField] private PlayerManager m_playerManager;
        public void Interact()
        {
            m_playerManager.IsInSpawnPoint = true;
            m_playerManager.StartLoadingBar();
        }

        public void UnInteract()
        {
            m_playerManager.IsInSpawnPoint = false;
            m_playerManager.ResetBar();
        }
    }
}