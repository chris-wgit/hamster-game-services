using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using PlayFab.ClientModels;

public class AuthenticateManager : MonoBehaviour
{
    [BoxGroup("Broadcast Event")]
    public LoadSceneEventSO LoadMainMenuEvent;

    public PlayerDataSO playerData;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        EventHandlerUI.ActiveLoadingWithProcess(true);
        EventHandlerUI.SetLoadingProcessEvent("Login...", 0.1f);
        PlayfabAuth.LoginWithCustomID(OnLoginSuccess);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        if (result.NewlyCreated)
        {
            EventHandlerPlayfab.UpdateUserNameAction(LoadUserData);
        }
        else
        {
            LoadUserData();
        }
    }

    private void LoadUserData()
    {
        EventHandlerUI.SetLoadingProcessEvent("Loading User Data", 0.4f);
        PlayfabData.LoadUserCombinedData(OnLoadDataCompleted);
    }

    private void OnLoadDataCompleted(GetPlayerCombinedInfoResultPayload data)
    {
        EventHandlerUI.SetLoadingProcessEvent("Loading Game Data", 0.8f);
        playerData.WriteCombinedData(data);
        StartCoroutine(WaitForWritingData());
    }

    private IEnumerator WaitForWritingData()
    {
        yield return new WaitForSeconds(1);
        LoadMainMenu();

    }

    private void LoadMainMenu()
    {
        LoadMainMenuEvent?.RaiseEvent();
    }




}
