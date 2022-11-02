// using Commands;
// using Controllers;
// using Datas.ValueObject;
// using Signals;
// using UnityEngine;
// using UnityTemplateProjects.Datas.UnityObjects;
//
// namespace Managers
// {
//     public class LevelManager : MonoBehaviour
//     {
//         #region Self Variables
//
//         #region Public Variables
//
//         [Header("Data")] public LevelData Data;
//
//         #endregion
//
//         #region Serialized Variables
//
//         [Space] [SerializeField] private GameObject levelHolder;
//         [SerializeField] private LevelLoaderCommand levelLoader;
//         [SerializeField] private ClearActiveLevelCommand levelClearer;
//
//         #endregion
//
//         #region Private Variables
//
//          private int _levelID;
//
//         #endregion
//
//         #endregion
//         
//
//         // private int GetActiveLevel()
//         // {
//         //    
//         // }
//
//         private LevelData GetLevelData()
//         {
//             var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelDataList.Count;
//             return Resources.Load<CD_Level>("Data/CD_Level").LevelDataList[newLevelData];
//         }
//
//         #region Event Subscription
//
//         private void OnEnable()
//         {
//             SubscribeEvents();
//         }
//         private void SaveLevelID(int levelID)
//         {
//             ///////////////SaveLoadSignals.Instance.onSaveLevelID?.Invoke(levelID);
//         }
//
//         private void OnLoadLevelID(int levelID)
//         {
//             _levelID = levelID;
//         }
//
//         private void SubscribeEvents()
//         {
//             CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
//             CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
//             CoreGameSignals.Instance.onNextLevel += OnNextLevel;
//             CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
//            
//         }
//
//         private void UnsubscribeEvents()
//         {
//             CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
//             CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
//             CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
//             CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
//             
//         }
//
//         private void OnDisable()
//         {
//             UnsubscribeEvents();
//         }
//
//         #endregion
//
//         private void Start()
//         {
//             ///////////////////_levelID = GetActiveLevel();
//             Data = GetLevelData();
//             OnInitializeLevel();
//         }
//
//         private void OnNextLevel()
//         {
//             _levelID++;
//             CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
//             CoreGameSignals.Instance.onReset?.Invoke();
//             CoreGameSignals.Instance.onLevelInitialize?.Invoke();
//         }
//
//         private void OnRestartLevel()
//         {
//             CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
//             CoreGameSignals.Instance.onReset?.Invoke();
//             CoreGameSignals.Instance.onLevelInitialize?.Invoke();
//         }
//
//         private int OnGetLevelID()
//         {
//             return _levelID;
//         }
//
//
//         private void OnInitializeLevel()
//         {
//             var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelDataList.Count;
//             levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
//         }
//
//         private void OnClearActiveLevel()
//         {
//             levelClearer.ClearActiveLevel(levelHolder.transform);
//         }
//     }
// }