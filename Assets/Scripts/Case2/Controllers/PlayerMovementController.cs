using Case2.Managers;
using UnityEngine;

namespace Case2.Controllers
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
                AlignPlayerToStacks(rigidbody,currentStack.position.x);
            }
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y,1.5f);
        }
        
        private void AlignPlayerToStacks(Rigidbody rigidbody,float desiredXPos)
        {
            float updatedXpos=Mathf.Lerp(_playerManager.transform.position.x, desiredXPos,0.02f);
            _playerManager.transform.position=new Vector3(updatedXpos,transform.position.y,transform.position.z);
        }

        public void StopWithoutY(Rigidbody playerRb)
        {
            playerRb.velocity = new Vector3(0,playerRb.velocity.y,0);
        }
    }
}