using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayfabAuth 
{
   public static void LoginWithCustomID(Action<LoginResult> onSuccess)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };

        PlayFabClientAPI.LoginWithCustomID(request, (success)=> onSuccess?.Invoke(success), OnPlayfabError);
    }

    private static void OnPlayfabError(PlayFabError error)
    {
        Debug.Log("Error");
    }

}

