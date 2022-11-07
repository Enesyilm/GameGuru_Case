using TMPro;
using UnityEngine;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;

        #endregion

        #region Private Variables

        private readonly string levelstring="Level: ";

        #endregion

        #endregion
        
        public void SetLevelText(int value)
        {
            levelText.text = levelstring+value.ToString();
            
        }

        
    }
}