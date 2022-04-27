using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UISwitchButton : MonoBehaviour
{
    [Header("Switch Component")]
    public Image swithBackground;
    public Image switchHandler;
    public GameObject switchHandlerObject;
    private Vector3 handlerPosition { get { return switchHandlerObject.transform.localPosition; } }

    public bool isActive;

    [Space(10)]
    [Header("Sprite")]
    public Sprite switchBG_On;
    public Sprite switchHandler_On;
    public Sprite switchBG_Off;
    public Sprite switchHandler_Off;

    public void OnSwitchButtonClicked()
    {
        isActive = !isActive;
        StartCoroutine(SwitchButtonEffect());
    }

    public void SetSwithState(bool state)
    {
        isActive = state;
        SetSwitchUI(isActive);
    }

    void SetSwitchUI(bool value)
    {
        switch (value)
        {
            case true:
                switchHandler.sprite = switchHandler_On;
                swithBackground.sprite = switchBG_On;
                if (handlerPosition.x < 0) 
                {
                    switchHandlerObject.transform.localPosition = new Vector3(-handlerPosition.x, handlerPosition.y, handlerPosition.z);
                }

                break;
            case false:
                switchHandler.sprite = switchHandler_Off;
                swithBackground.sprite = switchBG_Off;
                if (handlerPosition.x > 0)
                {
                    switchHandlerObject.transform.localPosition = new Vector3(-handlerPosition.x, handlerPosition.y, handlerPosition.z);
                }
                break;

        }
    }
    
    IEnumerator SwitchButtonEffect()
    {
        switchHandlerObject.transform.DOLocalMoveX(-handlerPosition.x, 0.3f);

        yield return new WaitForEndOfFrame();

        SetSwitchUI(isActive);
    }
}
