using AnimationMachine;
using Joystick_Pack.Examples;
using SavingSystem;
using UnityEngine;
namespace Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        public bool IsDied;
        public JoystickPlayerExample JoystickObject;
        [SerializeField] private GameObject m_loseCanvas;
        [SerializeField] private AnimatorManager m_animatorManager;
        [SerializeField] private SaveManager m_saveManager;
        public GameObject GetLoseScreenCanvas => m_loseCanvas;
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus && !IsDied)
            {
                m_saveManager.SaveAll();
            }
            else if(!hasFocus && IsDied)
            {
                PlayerPrefs.DeleteAll();
            }
        }
        public void SetActiveJoystick(bool isActive)
        {
            JoystickObject.enabled = isActive;
        }
        public void KillThePlayer()
        {
           JoystickObject.enabled = false;
           IsDied = true;
           StartCoroutine(m_animatorManager.SetDeathState());
           GetLoseScreenCanvas.SetActive(true);
           PlayerPrefs.DeleteAll();
        }
    }
}