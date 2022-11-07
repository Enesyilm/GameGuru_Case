using System.Collections.Generic;
using Concretes;
using Controller;
using UnityEngine;

namespace Case2.Controllers
{
    public class BlockSpawnController : MonoBehaviour
    {
        #region Self Variables
            
        #region Public Variables
            public GameObject blockPrefab;
        #endregion
            
        #region Serialized Variables
            [SerializeField] private int blockPoolAmount;
            [SerializeField] private List<Color> colorList;
        #endregion
            
        #region Private Variables
                private ObjectPool<PoolableObject> _blockPool;
                private int _colorindex=0;
                
        #endregion
            
        #endregion

        private void Awake()
        {
            _blockPool = new ObjectPool<PoolableObject>(blockPrefab,blockPoolAmount);
        }

        public void Reset()
        {
            _colorindex = 0;
        }

        public BlockController GetFromPool(Vector3 spawnPos)
        {
            spawnPos = spawnPos+new Vector3(0,0,blockPrefab.transform.localScale.z);
            PoolableObject newPoolableObject = _blockPool.Pull(spawnPos);
            BlockController _blockController = newPoolableObject.GetComponent<BlockController>();
            _blockController.MeshRenderer.material.color =colorList[_colorindex];
            _colorindex++;
            if (_colorindex > colorList.Count - 1) _colorindex=0;
            return _blockController;
        }
        public BlockController GetFromPoolFallingPart(Vector3 spawnPos)
        {
            spawnPos = spawnPos+new Vector3(0,0,blockPrefab.transform.localScale.z);
            PoolableObject newPoolableObject = _blockPool.Pull(spawnPos);
            BlockController _blockController = newPoolableObject.GetComponent<BlockController>();
            if(_colorindex-1>0)_blockController.MeshRenderer.material.color =colorList[_colorindex-1];
            else{_blockController.MeshRenderer.material.color =colorList[_colorindex];}
            return _blockController;
        }

    }
}