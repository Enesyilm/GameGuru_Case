using Extentions;
using UnityEngine.Events;

namespace Case1.Signals
{
    public class GridSignals:MonoSingleton<GridSignals>
    {
        public UnityAction<int> onCreateNewGrid=delegate(int arg0) {  };
        public UnityAction<int> onIncreaseMatchCount=delegate{  };
    }
}