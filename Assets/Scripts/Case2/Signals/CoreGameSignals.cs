using System;
using Case2.Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Case2.Signals
{
    public class CoreGameSignals:MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay=delegate {  };
        public UnityAction onInputTaken=delegate {  };
        public UnityAction<GameStates> onChangeGameStatus=delegate {  };
        public UnityAction onLevelFailed=delegate {  };
        public UnityAction onLevelSuccessful=delegate {  };
        public UnityAction onNextLevel=delegate {  };
        public UnityAction onRestartLevel=delegate {  };
        public Func<int> onGetLevelID= delegate { return 0;};
        public Func<Vector3> onGetStartBlock= delegate { return new Vector3();};
        public UnityAction<SaveType,int> onSaveData= delegate {};

    }
}