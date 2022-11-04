using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class GridController : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables
            public Vector2 CurrentIndex;
            public GridManager GridManager;
            public bool IsGridClicked;
        #endregion
        
        #region Serialized Variables
            [SerializeField] private GameObject xSprite;
            [SerializeField] private GameObject glowSprite;
            [SerializeField] TextMeshPro xText;
        #endregion

        #region Private Variables
            private Color _prevColor;
        #endregion

        #endregion

        private void Awake()
        {
            _prevColor=xText.color;
        }
        
        public void OnMouseDown()
        {
            if (!IsGridClicked)
            {
                xSprite.SetActive(true);
                IsGridClicked = true;
                GridManager.CheckAdjacentAmount((int)CurrentIndex.x,(int)CurrentIndex.y);
                OpenGlowSprite(true);
            }
        }
        
        public void ResetGrid()
        {
            IsGridClicked = false;
            xText.DOColor(Color.red, 0.5f).OnComplete((() =>
            {
                if(!IsGridClicked)xSprite.SetActive(false);
                xText.color = _prevColor;
                OpenGlowSprite(false);
            }));
        }

        public void OpenGlowSprite(bool state)
        {
            glowSprite.SetActive(state);
        }
        
    }
}