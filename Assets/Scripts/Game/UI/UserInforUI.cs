using DFT.DataManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInforUI : MonoBehaviour
{
    [Header("Username")]
    public TextMeshProUGUI userName;

    public StringVariableSO DisplayName;

    [Header("User Experience")]
    public TextMeshProUGUI LevelText;

    public TextMeshProUGUI ExperienceText;
    public Slider ExperienceSlider;

    public StatisticVariableSO Level;
    public StatisticVariableSO Experience;

    //public UserLevelLibrarySO LevelList;
    //private UserLevelSO currentLevel;

    //private void Start()
    //{
    //    userName.text = DisplayName.Value;
    //    SetLevel();
    //    SetExperience();
    //}

    //private void SetLevel()
    //{
    //    LevelText.text = Level.Value.ToString();
    //    currentLevel = LevelList.Level[Level.Value];
    //}

    //private void SetExperience()
    //{
    //    ExperienceSlider.maxValue = currentLevel.RequirementExperience;
    //    ExperienceSlider.value = Experience.Value;
    //    ExperienceText.text = Experience.Value.ToString() + "/" + currentLevel.RequirementExperience;
    //}
}