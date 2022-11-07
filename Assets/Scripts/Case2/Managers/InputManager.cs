using Case2.Signals;
using UnityEngine;

namespace Case2.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private bool _isFirstTimeTouchTaken=false;
        

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onRestartLevel+=OnRestartLevel;
            CoreGameSignals.Instance.onNextLevel+=OnRestartLevel;

        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onRestartLevel+=OnRestartLevel;
            CoreGameSignals.Instance.onNextLevel+=OnRestartLevel;
        }

        #endregion
        private void OnRestartLevel()
        {
            _isFirstTimeTouchTaken = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_isFirstTimeTouchTaken)
                {
                    _isFirstTimeTouchTaken = true;
                    CoreGameSignals.Instance.onPlay.Invoke();
                    return;
                }

                CoreGameSignals.Instance.onInputTaken.Invoke();
            }
            
        }
    }
}