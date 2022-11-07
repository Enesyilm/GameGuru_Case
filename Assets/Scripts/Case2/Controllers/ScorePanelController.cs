using TMPro;
using UnityEngine;

namespace Case2.Controllers
{
    public class ScorePanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] TextMeshProUGUI starAmountText;
        [SerializeField] TextMeshProUGUI GemAmountText;

        #endregion

        #endregion
        public void UpdateStarText(int starAmount)
        {
            starAmountText.text =starAmount.ToString();
        }
        public void UpdateGemText(int gemAmount)
        {
            GemAmountText.text = gemAmount.ToString();

        }
    }
}