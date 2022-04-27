using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoomController : MonoBehaviour
{
    public GameObject roomUI;
    public UITextBehaviour roomInfor;
    public GameObject startGameButton;
    public void ActiveRoomUI(bool value)
    {
        roomUI.SetActive(value);
    }

    public void SetRoomInfor(string value)
    {
        roomInfor.SetText(value);
    }

    public void ActiveStartGameButton()
    {
        startGameButton.SetActive(true);
    }
}
