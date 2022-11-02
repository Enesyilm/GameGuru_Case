using System;
using UnityEngine;

namespace Controller
{
    public class GridController : MonoBehaviour
    {

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject xSprite;
        

        #endregion

        #region Private Variables

        private bool _isGridClicked; 
        

        #endregion

        #endregion
        public void OnMouseDown()
        {
            if (!_isGridClicked)
            {
                xSprite.SetActive(true);
            }
        }
    }
}