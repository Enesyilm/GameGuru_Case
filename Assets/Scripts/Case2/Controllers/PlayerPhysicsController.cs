using Case2.Enums;
using Case2.Managers;
using Case2.Signals;
using UnityEngine;

namespace Case2.Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private PlayerManager _playerManager;
        

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Stack"))
            {
                _playerManager.CurrentStack=other.transform;
                _playerManager.RotatePlayer();
            }

            if (other.CompareTag("Finish"))
            {
                CoreGameSignals.Instance.onChangeGameStatus?.Invoke(GameStates.Win);
                _playerManager.CurrentType = PlayerStates.Win;
            }

            if (other.TryGetComponent(out CollectableController collectableController))
            {
                collectableController.gameObject.SetActive(false);
                AudioSignals.Instance.onPlayAudio?.Invoke(AudioTypes.Collectable);
                if (collectableController.CollectableType == CollectableType.Gem)
                {
                    ScoreSignals.Instance.onIncreaseScore?.Invoke(ScoreType.Gem);
                }
                else
                {
                    ScoreSignals.Instance.onIncreaseScore?.Invoke(ScoreType.Star);
                }
            }
        }
    }
}