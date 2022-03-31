using System.Collections;
using UnityEngine;

namespace AnimationMachine
{
    public class AnimatorManager : MonoBehaviour
    {
        public delegate void OnFightingState(bool isFighting);
        public OnFightingState OnFighting;

        public delegate void OnDeathState();
        public OnDeathState OnDeath;

        public delegate void MovementTree(float movementSpeed);
        public MovementTree OnMovement;

        private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.05f);
        
        
        public IEnumerator SetFightingState(bool isFighting)
        {
            yield return _waitForSeconds;
            OnFighting(isFighting);
        }

        public IEnumerator SetDeathState()
        {
            yield return _waitForSeconds;
            OnDeath();
        }

        public IEnumerator MovementTreeValue(float value)
        {
            yield return _waitForSeconds;
            OnMovement(value);
        }

    }
}