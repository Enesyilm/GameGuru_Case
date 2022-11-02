using System;
using Interfaces;
using UnityEngine;

namespace Concretes
{
    public class PoolableObject : MonoBehaviour, IPoolable<PoolableObject>, Controller.IPoolable<PoolableObject>
    {
        private Action<PoolableObject> returnToPool;

        private void OnDisable()
        {
            ReturnToPool();
        }

        public void Initialize(Action<PoolableObject> returnAction)
        {
            this.returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            returnToPool?.Invoke(this);
        }
    }
}