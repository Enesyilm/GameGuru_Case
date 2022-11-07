using Case1.Controller;
using UnityEngine;
using Case1.Signals;

namespace Case1.Managers
{
    public class UIManager:MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private GridCreatorPanelController _gridCreatorPanelController;
        #endregion
        #endregion

        private void Awake()
        {
            _gridCreatorPanelController.UpdateMatchCount(0);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void UnSubscribeEvents()
        {
            GridSignals.Instance.onIncreaseMatchCount -= OnIncreaseMatchCount;
        }

        private void SubscribeEvents()
        {
            GridSignals.Instance.onIncreaseMatchCount += OnIncreaseMatchCount;
        }
        #endregion
        private void OnIncreaseMatchCount(int matchCount)
        {
            _gridCreatorPanelController.UpdateMatchCount(matchCount);
        }

        public void CreateNewGrid()
        {
            _gridCreatorPanelController.CreateNewGrid();
        }
        
    }

        
    }