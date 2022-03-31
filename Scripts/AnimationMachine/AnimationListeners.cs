using UnityEngine;

namespace AnimationMachine
{
    public class AnimationListeners : MonoBehaviour
    {
        public AnimatorManager AnimatorManagerScript;
        private Animator _animator;
        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsFighting = Animator.StringToHash("isFighting");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            AnimatorManagerScript.OnFighting += OnFightingAnimation;
            AnimatorManagerScript.OnDeath += OnDeathAnimation;
            AnimatorManagerScript.OnMovement += OnMovementAnimation;

        }

        private void OnDestroy()
        {
            AnimatorManagerScript.OnFighting -= OnFightingAnimation;
            AnimatorManagerScript.OnDeath -= OnDeathAnimation;
            AnimatorManagerScript.OnMovement -= OnMovementAnimation;
        }


        public void OnMovementAnimation(float movementSpeed)
        {
            _animator.SetFloat(Movement, movementSpeed);
        }
        public void OnDeathAnimation()
        {
            _animator.SetBool(IsDead, true);
        }
        public void OnFightingAnimation(bool isFighting)
        {
            
            _animator.SetBool(IsFighting, isFighting);
        }
    
    
    }
}
