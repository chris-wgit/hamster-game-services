using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeOut : MonoBehaviour
{
    public GameObject timeoutPanel;
    public UITextBehaviour timeoutText;
    private bool isActive { get { return timeoutPanel.activeSelf; } }
    private void OnEnable()
    {
        EventHandlerGameplay.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        EventHandlerGameplay.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.WaitConnection)
        {
            timeoutPanel.SetActive(true);
        }
        else
        {
            timeoutPanel.SetActive(false);
        }
    }

    //private void Update()
    //{
    //    if (isActive)
    //    {
    //        timeoutText.SetText(GameplayController.instance.currentTimeOut.ToString());
    //    }
    //}
}
