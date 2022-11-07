using System.Collections.Generic;
using Case2.Controllers;
using Case2.Data.UnityObjects;
using Case2.Signals;
using Unity.Mathematics;
using UnityEngine;
using Case2.Data.ValueObjects;
using Case2.Enums;

namespace Case2.Managers
{
    public class BlockManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

            [SerializeField] private BlockSpawnController blockSpawnController;
            [SerializeField] private BlockController startBlock;
            [SerializeField] private GameObject finishBlock;

        #endregion

        #region Private Variables

        private List<BlockController> _blockList=new List<BlockController>();
        private List<BlockController> _fallingbBlockList=new List<BlockController>();
        private BlockController _lastIndex;
        private BlockController _prevIndex;
        private Transform _prevIndexTransform;
        private Transform _lastIndexTransform;
        private bool _isLevelFailed;
        private LevelData _levelData;
        private int _currentLevelTotalBlockAmount;
        private int _currentLevelId;
        private int _blockIteratorIndex;
        private float _bufferDistance=0.05f;
        private float _pitchMultiplier;
        private BlockController _currentfinishLine;
        #endregion
        
        #endregion

        #region Getter Setters

        public BlockController LastIndx {
            get
            {
                if (_blockList.Count - 1 > 0) _lastIndex=_blockList[_blockList.Count-1];
                return _lastIndex;
            }
        }
        public BlockController PrevIndx {
            get
            {
                if (_blockList.Count - 1 > 0) _prevIndex=_blockList[_blockList.Count-2];
                return _prevIndex;
            }
        }
        public Transform PrevIndxTr {
            get
            {
                if (_blockList.Count - 1 > 0) _prevIndexTransform=_blockList[_blockList.Count-2].transform;
                return _prevIndexTransform;
            }
        }
        public Transform LastIndxTr {
            get
            {
                if (_blockList.Count - 1 > 0) _lastIndexTransform=_blockList[_blockList.Count-1].transform;
                return _lastIndexTransform;
            }
        }

        #endregion

        private void Awake()
        {
            _blockList.Add(startBlock);
            _levelData = GetLevelData();
            SetLevelData();
            SpawnFinishLine();
        }
        private LevelData GetLevelData() => Resources.Load<CD_Level>("Data/CD_Level").LevelData;
        
        #region Event Subscription
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onInputTaken += OnInputTaken;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onGetStartBlock +=OnGetStartBlock ;

        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onInputTaken -= OnInputTaken;
            CoreGameSignals.Instance.onGetStartBlock -=OnGetStartBlock ;
        }

        private Vector3 OnGetStartBlock()
        {
            return startBlock.transform.position;
        }

        #endregion
        private void OnLevelFailed()
        {
            _isLevelFailed = true;
            
            if(!LastIndx.IsSettle)LastIndx.ActivateFalling();
        }

        private void OnNextLevel()
        {
            CoreGameSignals.Instance.onChangeGameStatus?.Invoke(GameStates.Level);
            SetLevelData();
            ResetAllBlockTypes();
            _blockList.Clear();
            _blockList.TrimExcess();
            _blockList.Add(startBlock);
            UpdateStartBlockPosition();
            _blockIteratorIndex = 0;
            _isLevelFailed = false;
            LastIndx.IsSettle = false;
            SpawnFinishLine();
        }

        private void UpdateStartBlockPosition()
        {
            startBlock.transform.position =new Vector3(
                _currentfinishLine.transform.position.x,startBlock.transform.position.y, _currentfinishLine.transform.position.z+ _currentfinishLine.transform.localScale.z+ (startBlock.transform.localScale.z/2-.1f)
                );
        }
        
        private void OnRestartLevel()
        {
            ResetAllBlockTypes();
            _blockList.Clear();
            _blockList.TrimExcess();
            _blockList.Add(startBlock);
            blockSpawnController.Reset();
            _blockIteratorIndex = 0;
            _isLevelFailed = false;
            LastIndx.IsSettle = false;
        }

        private void ResetAllBlockTypes()
        {
            for (int index = 1; index <_blockList.Count; index++)
            {
                _blockList[index].ResetBlock();
                
            }
            for (int index = 0; index <_fallingbBlockList.Count; index++)
            {
                _fallingbBlockList[index].ResetBlock();
                
            }
        }

        private void OnInputTaken()
        {
            if (_isLevelFailed) return;
            if (LastIndx.IsSettle)
            {
                SpawnBlock();
                return;
            }

            if (!CheckIfRemainingPartFailed())
            {
                if (!(Mathf.Abs(LastIndxTr.position.x - PrevIndxTr.position.x) <= _bufferDistance))
                {
                    ResetPerfectSnapCombo();
                    CutBlock();
                }
                else
                {
                    PerfectSnapBlock();
                }
                LastIndx.StopMovement();
                SpawnBlock();
            }
            else
            {
                _isLevelFailed = true;
                LastIndx.ActivateFalling();
            }
        }

        private void ResetPerfectSnapCombo()
        {
            _pitchMultiplier=0;
        }

        private void PerfectSnapBlock()
        {
            CollectableSignals.Instance.onCreateCollectableInRunTime?.Invoke(LastIndxTr.position+Vector3.up,CollectableType.Star);
            AudioSignals.Instance.onPlayPinchAudio(AudioTypes.PerfectBlockPlace,1+(.1f*_pitchMultiplier));
            _pitchMultiplier++;
            LastIndxTr.localScale =new Vector3(
                PrevIndxTr.localScale.x,
                LastIndxTr.localScale.y,
                LastIndxTr.localScale.z
                ) ;
            LastIndxTr.position = 
            new Vector3(
                PrevIndxTr.position.x,
                LastIndxTr.position.y,
                LastIndxTr.position.z);
        }

        private void OnPlay()
        {
            SetLevelData();
            SpawnBlock();
        }

        private void SpawnFinishLine()
        {
            GameObject go=Instantiate(finishBlock, CalculateFinishBlockIndex(), quaternion.identity);
            _currentfinishLine = go.GetComponent<BlockController>();
            CollectableSignals.Instance.onCreateCollectableInRunTime?.Invoke(_currentfinishLine.transform.position+Vector3.up,CollectableType.Gem);
            
        }

        private void SpawnBlock()
        {
            if (CheckLevelBlockAmountIsCompleted()) return;
            BlockController newBlockObject = blockSpawnController.GetFromPool(startBlock.transform.position+CalculateBlockIndex());
               if(_blockList.Count-1>0) newBlockObject.transform.localScale=new Vector3(LastIndxTr.localScale.x, newBlockObject.transform.localScale.y, newBlockObject.transform.localScale.z);
            _blockList.Add(newBlockObject);
        }

        private void SetLevelData()
        {
            _currentLevelId=CoreGameSignals.Instance.onGetLevelID.Invoke();
            _currentLevelTotalBlockAmount = _levelData.BlockLength[_currentLevelId];
        }
        private bool CheckLevelBlockAmountIsCompleted()
        {

            SetLevelData();
            _blockIteratorIndex++;
            if (_blockIteratorIndex <= _levelData.BlockLength[_currentLevelId])
            {
                
                return false;
            }
            return true;
        }
      
        private Vector3 CalculateBlockIndex()
        {
            Vector3 _currenBlockIndex;
             if (_blockList.Count - 1>0)
            {
                _currenBlockIndex=new Vector3(LastIndx.XClampSize, 0, (_blockList.Count-1)*LastIndxTr.localScale.z);
            }
            else
            {
                _currenBlockIndex= new Vector3(_blockList[_blockList.Count-1].XClampSize, 0, 0);
            }
             return _currenBlockIndex;
        }
        private Vector3 CalculateFinishBlockIndex()
        {
            Vector3 _currenBlockIndex =new Vector3(_blockList[0].transform.position.x,
                finishBlock.transform.position.y,
            (  startBlock.transform.position.z+(startBlock.transform.localScale.z * _currentLevelTotalBlockAmount))+startBlock.transform.localScale.z/2+finishBlock.transform.localScale.z-.1f);
                return _currenBlockIndex;
        }

        private void CutBlock()
        {
            BlockController _cuttedPart=CreateCuttedPart();
            ReSizeRemainingPart(_cuttedPart);
            RePositionRemainingPart(_cuttedPart);
        }

        private void RePositionRemainingPart(BlockController cuttedPart)
        {
                if (PrevIndxTr.position.x<LastIndxTr.position.x)
                {
                    LastIndxTr.position = new Vector3(
                        cuttedPart.transform.position.x - cuttedPart.transform.localScale.x -
                        (LastIndxTr.localScale.x - cuttedPart.transform.localScale.x) / 2
                        , cuttedPart.transform.position.y
                        , cuttedPart.transform.position.z
                    );
                }

                if (PrevIndxTr.position.x>LastIndxTr.position.x)
                {
                    LastIndxTr.position = new Vector3(
                        cuttedPart.transform.position.x - cuttedPart.transform.localScale.x +
                        (LastIndxTr.localScale.x + cuttedPart.transform.localScale.x) / 2
                        , cuttedPart.transform.position.y
                        , cuttedPart.transform.position.z
                    );
                }
        }

        private void ReSizeRemainingPart(BlockController cuttedPart)
        {
                LastIndxTr.localScale = new Vector3(
                     LastIndxTr.localScale.x-Mathf.Abs(cuttedPart.transform.localScale.x),
                     LastIndxTr.localScale.y,
                     LastIndxTr.localScale.z
                 );
        }

        private BlockController CreateCuttedPart()
        { 
            Vector3 _fallingBlockSpawnPos=Vector3.down;
                if(PrevIndxTr.position.x<LastIndxTr.position.x){
                    _fallingBlockSpawnPos = new Vector3(LastIndxTr.position.x+PrevIndxTr.localScale.x/2-CalculateCuttedPartSize()/2
                        ,PrevIndxTr.position.y
                        ,PrevIndxTr.position.z);}
                if(PrevIndxTr.position.x>LastIndxTr.position.x){
                    _fallingBlockSpawnPos = new Vector3(LastIndxTr.position.x-PrevIndxTr.localScale.x/2-CalculateCuttedPartSize()/2
                        ,PrevIndxTr.position.y
                        ,PrevIndxTr.position.z);
                }
                BlockController _cuttedGameObject=blockSpawnController.GetFromPoolFallingPart(_fallingBlockSpawnPos);
            _fallingbBlockList.Add(_cuttedGameObject);
            _cuttedGameObject.ActivateFalling();
            _cuttedGameObject.transform.localScale=new Vector3(CalculateCuttedPartSize(),_cuttedGameObject.transform.localScale.y,_cuttedGameObject.transform.localScale.z);
            return _cuttedGameObject;
        }

        private float CalculateCuttedPartSize()
        {
            float prevBlockPos =  PrevIndxTr.position.x;
           
            float _cuttedLength=LastIndxTr.position.x-prevBlockPos;
            return _cuttedLength;
        }

        private bool CheckIfRemainingPartFailed()
        {
            if (Mathf.Abs(LastIndxTr.position.x - PrevIndxTr.position.x) > (LastIndxTr.localScale.x + PrevIndxTr.localScale.x)/2)
                {
                    return true;
                }
            return false;
        }
    }
}