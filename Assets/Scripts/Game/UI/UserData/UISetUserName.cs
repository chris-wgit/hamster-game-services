using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class UISetUserName : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInputField;

    [SerializeField]
    private GameObject setUsernamePanel;

    public Action OnSetUsernameCompleted;

    private void OnEnable()
    {
        EventHandlerPlayfab.OnSetUserNameEvent += ActiveUsernamePanel;
    }

    private void OnDisable()
    {
        EventHandlerPlayfab.OnSetUserNameEvent -= ActiveUsernamePanel;
    }

    private void ActiveUsernamePanel(Action onSuccess)
    {
        OnSetUsernameCompleted = onSuccess;
        ToggleUsernamePanel(true);
    }
    public void OnSaveNameButtonClicked()
    {
        PlayfabData.UpdateUserDisplayName(nameInputField.text, OnCompleteEvent);

        EventHandlerUI.ActiveLoading(true);
    }
    public void OnCompleteEvent(string name)
    {
        OnSetUsernameCompleted?.Invoke();
        ToggleUsernamePanel(false);
        EventHandlerUI.ActiveLoading(false);
    }

    private void ToggleUsernamePanel(bool value)
    {
        setUsernamePanel.SetActive(value);
    }




    
}
