using Case2.Data.UnityObjects;
using Case2.Data.ValueObjects;
using Case2.Enums;
using Case2.Keys;
using Case2.Signals;
using UnityEngine;

namespace Case2.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _currentLevelID=0;
        private LevelData _levelData;
        private SaveGameDataParams _saveGameDataParams;
        #endregion

        #endregion
        private void Awake()
        {
            _saveGameDataParams.Level = (ES3.KeyExists("Level"))?ES3.Load<int>("Level"):0;
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameStatus += OnChangeGameStatus;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameStatus -= OnChangeGameStatus;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
             
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _levelData = GetLevelData();
        }

        private LevelData GetLevelData() => Resources.Load<CD_Level>("Data/CD_Level").LevelData;

        private void OnChangeGameStatus(GameStates gameStates)
        {
            if (gameStates != GameStates.Win) return;
            _saveGameDataParams.Level++;
            CoreGameSignals.Instance.onSaveData?.Invoke(SaveType.Level,_saveGameDataParams.Level);
            _saveGameDataParams.Level=_saveGameDataParams.Level%_levelData.BlockLength.Count;
        }

        private int OnGetLevelID()
        {
            return _saveGameDataParams.Level;
        }

    }
}