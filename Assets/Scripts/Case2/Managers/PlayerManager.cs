using System;
using Case2.Enums;
using Case2.Signals;
using UnityEngine;
using UnityTemplateProjects.Case2.Controllers;
using Random = UnityEngine.Random;

namespace Case2.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
            public Transform CurrentStack;
        #endregion
        
        #region Serialized Variables
            [SerializeField] Rigidbody playerRigidbody;

            [SerializeField] PlayerMovementController movementController;
            [SerializeField] PlayerAnimationController animationController;
        #endregion

        #region Private Variables

            private bool isReadyToPlay=false;
            private PlayerStates _currentType=PlayerStates.Idle;
        #endregion
        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay+=OnPlay;
        }

      
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay-=OnPlay;
        }

        #endregion
        private void OnPlay()
        {
            isReadyToPlay = true;
            ChangePlayerState(PlayerStates.Run);
            ChangeAnimation(PlayerStates.Run);
        }

        private void FixedUpdate()
        {
            switch (_currentType)
            {
                
                case PlayerStates.Idle:
                    if (isReadyToPlay)Move();
                    else Stop();
                        break;
                case PlayerStates.Fall:
                    
                    break;
                case PlayerStates.Run:
                    Move();
                    break;
                case PlayerStates.Win:
                    Stop();
                    break;
            }
        }

        private void Stop()
        {
            movementController.Stop(playerRigidbody);
        }

        private void ChangeAnimation(PlayerStates state)
        {
            animationController.ChangeAnimation(state);
        }
        private void Move()
        {
            CheckIfPlayerFalling();
            movementController.Move(playerRigidbody,CurrentStack);
        }

        private void CheckIfPlayerFalling()
        {
            if (playerRigidbody.velocity.y < -1)
            {
                CoreGameSignals.Instance.onChangeGameStatus?.Invoke(GameStates.Failed);
                ChangeAnimation(PlayerStates.Fall);
                ChangePlayerState(PlayerStates.Fall);

            }
        }

        private void ChangePlayerState(PlayerStates state)
        {
            _currentType = state;
        }
    }
}