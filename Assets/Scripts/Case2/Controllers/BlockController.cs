using UnityEngine;

namespace Case2.Controllers
{
    public class BlockController : MonoBehaviour
    {   
        #region Self Variables

        #region Public Variables
            public bool IsSettle=false;
            public float XClampSize=3;
            public MeshRenderer MeshRenderer;
        #endregion

        #region Serialized Variables

        [SerializeField]private Collider collider;

        

        #endregion
        #region Private Variables
            private float _blockSpeed=2;
            private Rigidbody _rigidbody;
            private Vector3 _defaultPos;
            private Vector3 _defaultScale;
        #endregion

        #endregion

        private void Awake()
        {
            _rigidbody=gameObject.AddComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _defaultPos=transform.position;
            _defaultScale=transform.localScale;
        }
        
        public void ResetBlock()
        {
            transform.position = _defaultPos;
            collider.enabled = true;
            IsSettle = false;
            transform.localScale = _defaultScale;
            _rigidbody.isKinematic=true;
            _rigidbody.useGravity=false;
            gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            if(!IsSettle) MoveOnXAxis();
        }

        private void MoveOnXAxis()
        {
            if (Mathf.Abs(transform.position.x - XClampSize) < 0.1f)
            {
                XClampSize=XClampSize * -1;
                _blockSpeed=_blockSpeed * -1;
            }
            transform.Translate(_blockSpeed * Time.deltaTime, 0, 0);
        }

        public void StopMovement()
        {
            
            IsSettle = true;
        }

        public void ActivateFalling()
        {
            IsSettle = true;
            _rigidbody.useGravity=true;
            _rigidbody.isKinematic=false;
            collider.enabled = false;

        }
        
    }
}