using Case2.Enums;
using Extentions;
using UnityEngine.Events;

namespace Case2.Signals
{
    public class UISignals:MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onClosePanel=delegate {  };
        public UnityAction<UIPanels> onOpenPanel=delegate {  };
        public UnityAction<int> onSetLevelText=delegate {  };
    }
}