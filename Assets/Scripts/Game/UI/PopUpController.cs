using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    public GameObject _UIPopUp;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI notificationText;

    private void OnEnable()
    {
        EventHandlerUI.OnShowPopUpEvent += ShowPopUp;
    }
    private void OnDisable()
    {
        EventHandlerUI.OnShowPopUpEvent -= ShowPopUp;
    }

    public void ShowPopUp(string title, string content)
    {
        _UIPopUp.SetActive(true);
        errorText.text = title;
        notificationText.text = content;
    }
}
