using System;
using Case2.Enums;
using Case2.Managers;
using Case2.Signals;
using UnityEngine;

namespace UnityTemplateProjects.Case2.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
            [SerializeField] private PlayerManager _playerManager;
        #endregion
        
        #endregion
        public void Stop(Rigidbody rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
        }

        public void Move(Rigidbody rigidbody, Transform currentStack)
        {
            if (currentStack != null)
            {
                AlignPlayerToStacks(currentStack.position.x);
                RotatePlayer(currentStack);
            }
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y,3);
        }

        private void RotatePlayer(Transform target)
        {
            var lookPos = target.position - _playerManager.transform.position;
            // lookPos.x = 0;
            //lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(lookPos);
            //_playerManager.transform.rotation = Quaternion.Slerp(_playerManager.transform.rotation, rotation, Time.deltaTime * .1f);
        }

        private void AlignPlayerToStacks(float desiredXPos)
        {
                float updatedXpos=Mathf.Lerp(_playerManager.transform.position.x, desiredXPos,0.1f);
            _playerManager.transform.position=new Vector3(updatedXpos,transform.position.y,transform.position.z);
        }
    }
}