using System;
using Case2.Managers;
using UnityEngine;

namespace UnityTemplateProjects.Case2.Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private PlayerManager _playerManager;
        

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Stack"))
            {
                _playerManager.CurrentStack=other.transform;
            }
        }
    }
}