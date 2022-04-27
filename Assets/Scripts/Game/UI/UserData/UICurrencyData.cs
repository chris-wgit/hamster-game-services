using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurrencyData : MonoBehaviour
{
    [Header("Currency Data")]
    public PlayerDataSO data;

    public CurrencyEnum currencyType;

    [Header("UI Component")]
    public TextMeshProUGUI currencyText;

    public enum CurrencyEnum
    {
        Gold,
        Gem
    }


    private void OnEnable()
    {
        switch (currencyType)
        {
            case CurrencyEnum.Gold:
                data._OnGoldChanged += UpdateUI;
                break;
            case CurrencyEnum.Gem:
                data._OnGemChanged += UpdateUI;
                break;
        }
    }

    private void OnDisable()
    {
        switch (currencyType)
        {
            case CurrencyEnum.Gold:
                data._OnGoldChanged -= UpdateUI;
                break;
            case CurrencyEnum.Gem:
                data._OnGemChanged -= UpdateUI;
                break;
        }
    }

    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        //Get current Value
        int currentValue = int.Parse(currencyText.text);

        int newValue = 0;
        switch (currencyType)
        {
            case CurrencyEnum.Gold:

                newValue = data.gold;
                break;

            case CurrencyEnum.Gem:
                newValue = data.gem;
                break;
        }

        //Update data from current value to new value;
        UITextFX(currentValue, newValue);
    }

    private void UITextFX(int currentValue, int newValue)
    {
        //TODO: Currency Text FX

        // Animation for increasing and decreasing of currencies amount
        StartCoroutine(UpdateTextFX(currentValue, newValue));
      
    }

    private IEnumerator UpdateTextFX(int currentValue, int newValue)
    {
        // Animation for increasing and decreasing of currencies amount
        const float seconds = 0.5f;

        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            currencyText.text = Mathf.Floor(Mathf.Lerp(currentValue, newValue, (elapsedTime / seconds))).ToString();

            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();

        }

        currencyText.text = newValue.ToString();
    }

}


