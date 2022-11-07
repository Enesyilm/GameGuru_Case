using System;
using TMPro;
using UnityEngine;
using Case1.Signals;

namespace Case1.Controller
{
    public class GridCreatorPanelController:MonoBehaviour
    {
        #region Self Variables

            #region Serialized Variables
                [SerializeField] private TextMeshProUGUI _matchCountText;
                [SerializeField] private TMP_InputField gridSizeInput;
            #endregion
        #endregion

        public void UpdateMatchCount(int matchCount)
        {
            _matchCountText.text = "Match Count :" +matchCount;
        }
        public void CreateNewGrid()
        {
            if (gridSizeInput.text==""||Int32.Parse(gridSizeInput.text)<2) return;
            GridSignals.Instance.onCreateNewGrid.Invoke(Int32.Parse(gridSizeInput.text));
        }
    }
}