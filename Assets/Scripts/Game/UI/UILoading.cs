using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILoading : MonoBehaviour
{
    [SerializeField] private GameObject loadingObject;
    [SerializeField] private TextMeshProUGUI inforText;
    private void OnEnable()
    {
        EventHandlerUI.OnShowLoadingEvent += ActiveLoading;
    }
    private void OnDisable()
    {
        EventHandlerUI.OnShowLoadingEvent -= ActiveLoading;
    }
    public void ActiveLoading(bool value, string loadingInfor)
    {
        loadingObject.SetActive(value);
        SetLoadingInfor(loadingInfor);
    }

    public void SetLoadingInfor(string value)
    {
        inforText.text = value;
    }
}
