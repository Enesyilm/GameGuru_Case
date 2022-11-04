using Case2.Enums;
using Extentions;
using UnityEngine.Events;

namespace Case2.Signals
{
    public class CoreGameSignals:MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay=delegate {  };
        public UnityAction<GameStates> onChangeGameStatus=delegate {  };
    }
}