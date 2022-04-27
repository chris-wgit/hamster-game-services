using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIMainMenu : Singleton<UIMainMenu>
{
    [Header("Panel")]
    public GameObject PanelLobby;
    public GameObject PanelCharacterLibrary;
    public GameObject PanelCharacterDetails;

    public GameObject[] allPanel;

    public GameplaySO[] gameModes;

    private void Start()
    {
        //OnShowMainMenu();
    }


    public void OnShowMainMenu()
    {
        for (int i = 0; i < allPanel.Length; i++)
        {
            allPanel[i].SetActive(false);
        }
    }

    public void ActiveCharacterLibrary()
    {
        PanelCharacterLibrary.SetActive(true);
    }

    public void ActiveCharacterDetails()
    {
        PanelCharacterDetails.SetActive(true);
    }

    public void SwitchGameMode(int id)
    {
        GameInstance.Instance.SetGameMode(gameModes[id]);
    }
}

