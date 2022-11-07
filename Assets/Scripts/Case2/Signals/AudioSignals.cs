using Case2.Enums;
using Extentions;
using UnityEngine.Events;

namespace Case2.Signals
{
    public class AudioSignals : MonoSingleton<AudioSignals>
    {
        public UnityAction<AudioTypes> onPlayAudio=delegate(AudioTypes arg0) {  };
        public UnityAction<AudioTypes,float> onPlayPinchAudio=delegate {  };
    }
}