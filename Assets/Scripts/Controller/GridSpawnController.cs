using System.Collections.Generic;
using Concretes;
using UnityEngine;

namespace Controller
{
    public class GridSpawnController:MonoBehaviour
    {
            #region Self Variables
    
            #region Public Variables
                
                public GameObject gridPrefab;
                public List<PoolableObject> _gridList;
            #endregion
            #region Serialized Variables
             [SerializeField] private int gridPoolAmount;
            #endregion

            #region Private Variables
            private ObjectPool<PoolableObject> _gridPool;
            

            #endregion
            #endregion

            private void Awake()
            {
                _gridList=new List<PoolableObject>();
                _gridPool = new ObjectPool<PoolableObject>(gridPrefab,gridPoolAmount);
            }
        
            private void Start()
            {
                
            }

            public void GetFromPool(Vector3 spawnPos)
            {
                PoolableObject newPoolableObject = _gridPool.Pull(spawnPos);
                _gridList.Add(newPoolableObject);
            }

            public void ResetPool()
            {
                for (int index = 0; index < _gridList.Count; index++)
                {
                    _gridList[index].gameObject.SetActive(false);
                }
            }
    }
}