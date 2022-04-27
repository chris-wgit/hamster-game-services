using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISliderBehaviour : MonoBehaviour
{
    [Header("UI References")]
    public Slider slider;
    public UITextBehaviour textSliderDisplay;

    [SerializeField] private float tweenDuration=1f;

    private void Awake()
    {
        if (slider == null) slider = GetComponent<Slider>();
    }

    public void SetupDisplay(float totalValue)
    {
        SetMaxValue(totalValue);
    }

    public void SetupDisplay(int totalValue)
    {
        SetMaxValue(totalValue);
    }

    public void SetMaxValue(float newValue)
    {
        slider.maxValue = newValue;
    }

    public void SetMaxValue(int newValue)
    {
        slider.maxValue = newValue;
    }

    public void SetCurrentValue(int newValue)
    {
        slider.value = newValue;
        SetTextDisplay();
    }

    public void SetCurrentValue(float newValue)
    {
        slider.value = newValue;
        SetTextDisplay();
    }

    void SetTextDisplay()
    {
        if (textSliderDisplay != null)
        {
            textSliderDisplay.SetText(slider.value.ToString());
        }
    }

    public void SetCurrentValueAnimation(int newValue)
    {
        slider.DOValue(newValue, tweenDuration, true);
    }

}
