using Case2.Enums;
using UnityEngine;

namespace Case2.Controllers
{
    public class CollectableController:MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public CollectableType CollectableType; 

        #endregion

        #endregion
        private void Update()
        {
            transform.Rotate(0,1,0);
        }
    }
}