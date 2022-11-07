using Case2.Enums;
using Case2.Keys;
using Case2.Signals;
using UnityEngine;

namespace Case2.Managers
{
    public class CoreGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

           private SaveGameDataParams _saveGameDataParams;


           #endregion
       
           #endregion
       
           private void Awake()
           {
               _saveGameDataParams.Level=(ES3.KeyExists("Level")) ? ES3.Load<int>("Level") : 0;
               _saveGameDataParams.Gem=(ES3.KeyExists("Gem")) ? ES3.Load<int>("Gem") : 0;
               _saveGameDataParams.Star=(ES3.KeyExists("Star")) ? ES3.Load<int>("Star") : 0;
               Application.targetFrameRate = 60;
           }
       
       
           private void OnEnable()
           {
               SubscribeEvents();
           }
       
       
           private void SubscribeEvents()
           {
               CoreGameSignals.Instance.onChangeGameStatus += OnChangeGameState;
               CoreGameSignals.Instance.onSaveData += OnAssignData;


           }
       
           private void UnsubscribeEvents()
           {
               CoreGameSignals.Instance.onChangeGameStatus -= OnChangeGameState;
               CoreGameSignals.Instance.onSaveData -= OnAssignData;
           }
        
           private void OnDisable()
           {
               UnsubscribeEvents();
           }
       
           private void OnChangeGameState(GameStates newState)
           {
               if (newState == GameStates.Win)
               {
                   CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
               }
           }
           private void OnSaveGame(SaveGameDataParams saveDataParams)
           {
               ES3.Save("Level", saveDataParams.Level);
               ES3.Save("Gem", saveDataParams.Gem);
               ES3.Save("Star", saveDataParams.Star);
           }

           private void OnAssignData(SaveType saveDataParams,int Amount)
           {
               switch (saveDataParams)
               {
                   case SaveType.Gem:
                       _saveGameDataParams.Gem = Amount;
                       break;
                   case SaveType.Star:
                       _saveGameDataParams.Star = Amount;
                       break;
                   case SaveType.Level:
                       _saveGameDataParams.Level = Amount;
                       break;
               }
           }

           private void OnApplicationPause(bool pauseStatus)
           {
               OnSaveGame(_saveGameDataParams);
           }

           private void OnApplicationQuit()
           {
                     
               OnSaveGame(_saveGameDataParams); 
           }
                     
    }
}
                     
