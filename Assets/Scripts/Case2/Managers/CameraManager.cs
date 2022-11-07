using Case2.Enums;
using Case2.Signals;
using UnityEngine;

namespace Case2.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

            [SerializeField] private Animator animator;

            [SerializeField] private GameObject orbitalCam;


        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameStatus += OnChangeGameStatus;
        }

        private void OnChangeGameStatus(GameStates arg0)
        {
            ChangeCameraState(arg0);
        }

        public void ChangeCameraState(GameStates arg0)
        {
            if (arg0 == GameStates.Win)
            {
                orbitalCam.SetActive(true);
                animator.Play(arg0.ToString());
            }
            if(GameStates.Level==arg0)
            {
                animator.Play(arg0.ToString());
                orbitalCam.SetActive(false);
            }
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameStatus -= OnChangeGameStatus;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
    }
}