using Case2.Controllers;
using Case2.Enums;
using Case2.Signals;
using Controllers;
using UnityEngine;


namespace Case2.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private ScorePanelController scorePanelController;

        #endregion

        #region Private Variables

        private int _currentLevelID=0;

        #endregion

        #endregion
        private void Awake()
        {
            UpdateGemScore(0);
            UpdateStarScore(0);
            
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            ScoreSignals.Instance.onSendScoreToManagers+=OnSendScoreToManagers;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        }
        
        private void UnsubscribeEvents()
        {
            
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            ScoreSignals.Instance.onSendScoreToManagers-=OnSendScoreToManagers;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _currentLevelID=CoreGameSignals.Instance.onGetLevelID.Invoke();
            OnSetLevelText(_currentLevelID);
        }

        private void OnSendScoreToManagers(ScoreType scoreType, int scoreAmount)
        {
            if (scoreType == ScoreType.Gem)
            {
                UpdateGemScore(scoreAmount);
            }
            else
            {
                UpdateStarScore(scoreAmount);
            }
        }

        private void OnNextLevel()
        {
            _currentLevelID=CoreGameSignals.Instance.onGetLevelID.Invoke();
            OnSetLevelText(_currentLevelID);
        }

        private void UpdateStarScore(int scoreAmount)
        {
           scorePanelController.UpdateStarText(scoreAmount);
        }

        private void UpdateGemScore(int scoreAmount)
        {
            scorePanelController.UpdateGemText(scoreAmount);
        }

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }
        
        private void OnSetLevelText(int value)
        {
            
            levelPanelController.SetLevelText(value);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LosePanel);
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.WinPanel);
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LosePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }
    }
}