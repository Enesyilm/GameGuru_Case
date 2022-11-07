using Concretes;
using UnityEngine;
using Case2.Enums;
using Case2.Signals;
using Controller;

namespace Case2.Controllers
{
    public class CollectableSpawnController : MonoBehaviour
    {
        #region Self Variables
                    
        #region Public Variables
        public GameObject StarPrefab;
        public GameObject GemPrefab;
        #endregion
        
        #region Serialized Variables
        [SerializeField] private int gridPoolAmount;
        #endregion
        
        #region Private Variables
        private ObjectPool<PoolableObject> StarPool;
        private ObjectPool<PoolableObject> GemPool;
        #endregion
        
        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onCreateCollectableInRunTime+=OnCreateCollectableInRunTime;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onCreateCollectableInRunTime-=OnCreateCollectableInRunTime;
        }

        #endregion
        private void Awake()
        {
            StarPool = new ObjectPool<PoolableObject>(StarPrefab,gridPoolAmount);
            GemPool = new ObjectPool<PoolableObject>(GemPrefab,gridPoolAmount);
        }
        private void OnCreateCollectableInRunTime(Vector3 pos,CollectableType collectableType)
        {
            if (collectableType == CollectableType.Gem)
            {
                GetGemFromPool(pos);
            }
            else
            {
                GetStarFromPool(pos);
            }
        }
        public void GetStarFromPool(Vector3 spawnPos)
        {
            PoolableObject newPoolableObject = StarPool.Pull(spawnPos);    }
        public void GetGemFromPool(Vector3 spawnPos)
        {
            PoolableObject newPoolableObject = GemPool.Pull(spawnPos);    }

    }
}