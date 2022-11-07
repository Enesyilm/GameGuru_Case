using UnityEngine;
using Case2.Enums;

namespace Case2.Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
            [SerializeField] private Animator animator;
        #endregion

        #region Private Variables

        private PlayerStates _currentAnimation=PlayerStates.Idle;
        

        #endregion
        #endregion

        public void ChangeAnimation(PlayerStates state)
        {
            if (_currentAnimation == state) return;
            _currentAnimation = state;
            animator.SetTrigger(state.ToString());
        }
    }
}