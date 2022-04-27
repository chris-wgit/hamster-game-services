using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingScene : MonoBehaviour
{

    public GameObject loadingObject;

    public TextMeshProUGUI loadingText;

    public Slider loadingSlider;

    private void OnEnable()
    {
        EventHandlerUI.OnShowLoadingWithLoadProcessEvent += ActiveLoadingScene;
        EventHandlerUI.OnSetLoadingProcessEvent += SetLoadingInfor;
    }
    private void OnDisable()
    {
        EventHandlerUI.OnShowLoadingWithLoadProcessEvent -= ActiveLoadingScene;
        EventHandlerUI.OnSetLoadingProcessEvent -= SetLoadingInfor;
    }
    public void ActiveLoadingScene(bool value)
    {
        if(loadingObject != null) loadingObject.SetActive(value);
    }

    public void SetLoadingInfor(string textValue, float value)
    {
        loadingText.text = textValue;

        loadingSlider.value = value;
    }
}
