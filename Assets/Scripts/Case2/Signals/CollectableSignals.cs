using Case2.Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Case2.Signals
{
    public class CollectableSignals:MonoSingleton<CollectableSignals>
    {
        public UnityAction<Vector3,CollectableType> onCreateCollectableInRunTime=delegate {  };
    }
}