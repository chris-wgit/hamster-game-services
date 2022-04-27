using UnityEngine;
using TMPro;
using DFT.DataManager;


namespace DesertFoxTeam
{
    public class UserStatsUI : MonoBehaviour
    {
        public PlayerDataSO playerData;
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI gemText;
        [Header("Event")]
        public VoidEventChannelSO OnCurrencyVariableChanged;

        private void OnEnable()
        {
            OnCurrencyVariableChanged.OnEventRaised += UpdateDisplay;
        }
        private void OnDestroy()
        {
            OnCurrencyVariableChanged.OnEventRaised -= UpdateDisplay;
        }
        private void Start()
        {
            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            coinText.text = playerData.gold.ToString();
            gemText.text = playerData.gem.ToString();
        }
    }
}

