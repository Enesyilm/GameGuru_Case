using System;
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
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (!_isFirstTimeTouchTaken)
                {
                    _isFirstTimeTouchTaken = true;
                    CoreGameSignals.Instance.onPlay.Invoke();
                }
            }
            
        }
    }
}