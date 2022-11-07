using Case2.Enums;
using Extentions;
using UnityEngine.Events;

namespace Case2.Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<ScoreType> onIncreaseScore=delegate(ScoreType arg0) {  };
        public UnityAction<ScoreType,int> onSendScoreToManagers= delegate{};
        
    }
}