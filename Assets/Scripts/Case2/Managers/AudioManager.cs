using System.Collections.Generic;
using Case2.Enums;
using Case2.Signals;
using UnityEngine;

namespace Case2.Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] List<AudioClip> _audioClips=new List<AudioClip>() ;

        [SerializeField] private AudioSource _audioSource;


        #endregion

        #region Private Variables

        private int _pinchMultiplierIndex;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            AudioSignals.Instance.onPlayAudio += OnPlayAudio;
            AudioSignals.Instance.onPlayPinchAudio += OnPlayPinchAudio;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            AudioSignals.Instance.onPlayAudio -= OnPlayAudio;
            AudioSignals.Instance.onPlayPinchAudio -= OnPlayPinchAudio;
        }
        #endregion

        private void OnLevelSuccessful()
        {
            OnPlayAudio(AudioTypes.Win);
        }

        private void OnPlayAudio(AudioTypes audioType)
        {
            _audioSource.pitch = 1;
            _audioSource.clip = _audioClips[(int)audioType];
            _audioSource.Play();
        } 
        
        private void OnPlayPinchAudio(AudioTypes audioType,float pitchMultiplier)
        {
            _audioSource.clip = _audioClips[(int)audioType];
            _audioSource.pitch = pitchMultiplier;
            _audioSource.Play();
        }

    }
}