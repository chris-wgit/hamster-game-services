using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExperienceInfor : MonoBehaviour
{
    public PlayerDataSO _playerData;

    public UISliderBehaviour expSlider;


    void Start()
    {
        UpdatePlayerExp();
    }

    void UpdatePlayerExp()
    {
        expSlider.SetMaxValue(_playerData.TotalExpRequirement);
        expSlider.SetCurrentValue(_playerData.experience);
    }
}
