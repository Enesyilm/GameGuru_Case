using Case2.Enums;
using Case2.Keys;
using Case2.Signals;
using UnityEngine;

namespace Case2.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables
        
        private SaveGameDataParams _saveGameDataParams;

        #endregion

        #endregion

        private void Awake()
        {
            _saveGameDataParams.Gem = (ES3.KeyExists("Gem"))?ES3.Load<int>("Gem"):0;
            _saveGameDataParams.Star = (ES3.KeyExists("Star"))?ES3.Load<int>("Star"):0;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onIncreaseScore+=OnIncreaseScore;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onIncreaseScore-=OnIncreaseScore;
        }

        #endregion

        private void Start()
        {
            ScoreSignals.Instance.onSendScoreToManagers?.Invoke(ScoreType.Gem,_saveGameDataParams.Gem);
            ScoreSignals.Instance.onSendScoreToManagers?.Invoke(ScoreType.Star,_saveGameDataParams.Star);
        }

        private void OnIncreaseScore(ScoreType scoreType)
        {
            if (scoreType == ScoreType.Gem)
            { 
                _saveGameDataParams.Gem++;
                ScoreSignals.Instance.onSendScoreToManagers.Invoke(ScoreType.Gem,_saveGameDataParams.Gem);
                CoreGameSignals.Instance.onSaveData?.Invoke(SaveType.Gem,_saveGameDataParams.Gem);
           }
            else
            {
                _saveGameDataParams.Star++;
                CoreGameSignals.Instance.onSaveData?.Invoke(SaveType.Star,_saveGameDataParams.Star);
                ScoreSignals.Instance.onSendScoreToManagers.Invoke(ScoreType.Star,_saveGameDataParams.Star);
            }
        }
    }
}