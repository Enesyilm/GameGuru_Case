using Concretes;
using UnityEngine;

namespace Controller
{
    public class GridSpawnController:MonoBehaviour
    {
            #region Self Variables
            
            #region Public Variables
                public GameObject gridPrefab;
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
                _gridPool = new ObjectPool<PoolableObject>(gridPrefab,gridPoolAmount);
            }
            
            public GridController GetFromPool(Vector3 spawnPos)
            {
                PoolableObject newPoolableObject = _gridPool.Pull(spawnPos);
                GridController _gridController = newPoolableObject.GetComponent<GridController>();
                return _gridController;
            }

            
    }
}