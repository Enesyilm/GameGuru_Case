using Case2.Enums;
using Case2.Signals;
using UnityEngine;
using Case2.Controllers;
using Unity.Mathematics;

namespace Case2.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
            public Transform CurrentStack;
            public PlayerStates CurrentType=PlayerStates.Idle;
        #endregion
        
        #region Serialized Variables
            [SerializeField] Rigidbody playerRigidbody;

            [SerializeField] PlayerMovementController movementController;
            [SerializeField] PlayerAnimationController animationController;
        #endregion

        #region Private Variables

            private bool isReadyToPlay=false;
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
            CoreGameSignals.Instance.onRestartLevel+=OnRestartLevel;
            CoreGameSignals.Instance.onNextLevel+=OnNextLevel;

        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay-=OnPlay;
            CoreGameSignals.Instance.onRestartLevel-=OnRestartLevel;
            CoreGameSignals.Instance.onNextLevel-=OnNextLevel;

        }

        #endregion

        private void OnNextLevel()
        {
            ChangeAnimation(PlayerStates.Idle);
            ChangePlayerState(PlayerStates.Idle);
            isReadyToPlay = false;
        }
        private void OnPlay()
        {
            isReadyToPlay = true;
            ChangePlayerState(PlayerStates.Run);
            ChangeAnimation(PlayerStates.Run);
        }
        private void OnRestartLevel()
        {
            transform.position=CoreGameSignals.Instance.onGetStartBlock.Invoke();
            transform.rotation = quaternion.identity;
            isReadyToPlay = false;
            CurrentType = PlayerStates.Idle;
            ChangeAnimation(PlayerStates.Idle);
        }
        private void FixedUpdate()
        {
            switch (CurrentType)
            {
                
                case PlayerStates.Idle:
                    if (isReadyToPlay)Move();
                    else Stop();
                        break;
                case PlayerStates.Fall:
                    CoreGameSignals.Instance.onChangeGameStatus?.Invoke(GameStates.Failed);
                    StopWithoutY();
                    break;
                case PlayerStates.Run:
                    Move();
                    break;
                case PlayerStates.Win:
                    Stop();
                    
                    ChangeAnimation(PlayerStates.Win);
                    break;
            }
        }

        private void Stop()
        {
            movementController.Stop(playerRigidbody);
        }
        private void StopWithoutY()
        {
            movementController.StopWithoutY(playerRigidbody);
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

        public void RotatePlayer()
        {
        }

        private void CheckIfPlayerFalling()
        {
            if (playerRigidbody.velocity.y < -2)
            {
                CoreGameSignals.Instance.onChangeGameStatus?.Invoke(GameStates.Failed);
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
                AudioSignals.Instance.onPlayAudio?.Invoke(AudioTypes.Failed);
                ChangeAnimation(PlayerStates.Fall);
                ChangePlayerState(PlayerStates.Fall);

            }
        }

        private void ChangePlayerState(PlayerStates state)
        {
            CurrentType = state;
        }
    }
}