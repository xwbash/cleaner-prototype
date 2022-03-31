using Interactor;
using UnityEngine;

namespace Players
{
    public class PlayerTriggersInteract : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other?.GetComponent<IInteractable>().Interact();
        }

        private void OnTriggerExit(Collider other)
        {
            other?.GetComponent<IInteractable>().UnInteract();
        }
    }
}
