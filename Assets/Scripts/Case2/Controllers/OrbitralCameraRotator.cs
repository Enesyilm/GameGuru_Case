using Cinemachine;
using UnityEngine;

namespace Case2.Controllers
{
    public class OrbitralCameraRotator:MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField]private CinemachineVirtualCamera ortbitralCam;
        #endregion
        #region Private Variables
            private CinemachineOrbitalTransposer _orbitalTransposer;
        #endregion

        #endregion
        
        private void Awake()
        {
            _orbitalTransposer = ortbitralCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        private void OnEnable()
        {
            ResetAxis();
        }

        public void ResetAxis()
        {
            _orbitalTransposer.m_XAxis.Value = 0;
        }
        
        private void Update()
        {
            
          
            if ((!ortbitralCam.enabled))
            {
                gameObject.SetActive(false);
                ResetAxis();
                
                
                return;
            }

            _orbitalTransposer.m_XAxis.Value += Time.deltaTime*_orbitalTransposer.m_XAxis.m_MaxSpeed;
        }
    }
}