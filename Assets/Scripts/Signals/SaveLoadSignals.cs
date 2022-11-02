using UnityEngine.Events;
using System;
using Datas.ValueObject;
using Extentions;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        
        public UnityAction<LevelData,int> onSaveGameData = delegate { };
        
        public Func<string, int, LevelData> onLoadGameData;
    }
}