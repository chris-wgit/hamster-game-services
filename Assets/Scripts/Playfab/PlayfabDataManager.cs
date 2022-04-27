using PlayFab;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayfabDataManager : Singleton<PlayfabDataManager>
{
    public PlayerDataSO playerData;
    private bool isBusy = false;

    #region EVENT

    [Header("Broadcast Event")]
    public UnityAction<GetPlayerCombinedInfoResultPayload> OnLoadUserCombinedDataSuccessEvent;
    public UnityAction<UserAccountInfo> OnGetUserAccountInforSuccessEvent;
    public UnityAction<List<StatisticValue>> OnGetPlayerStatisticSuccessEvent;
    public UnityAction<Dictionary<string, UserDataRecord>> OnGetUserDataSuccessEvent;
    public UnityAction OnGetUserInventorySuccessEvent;
    public UnityAction OnUpdateUserAccountInforSuccessEvent;
    public UnityAction<string> OnUpdateDisplayNameSuccessEvent;
    public UnityAction OnUpdatePlayerStatisticSuccessEvent;
    public UnityAction OnUpdateUserDataSuccessEvent;
    public UnityAction OnUpdateUserInventorySuccessEvent;
    #endregion

    #region LOAD_USER_COMBINED_DATA
    public void LoadUserCombinedData(UnityAction<GetPlayerCombinedInfoResultPayload> onSuccess)
    {
        OnLoadUserCombinedDataSuccessEvent = onSuccess;

        GetPlayerCombinedInfoRequestParams requestParams = new GetPlayerCombinedInfoRequestParams();
        requestParams.GetUserAccountInfo = true;
        requestParams.GetPlayerStatistics = true;
        requestParams.GetUserData = true;
        requestParams.GetUserInventory = true;
        
        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest() { InfoRequestParameters = requestParams };
          
        PlayFabClientAPI.GetPlayerCombinedInfo(request, OnGetPlayerCombinedInforSuccess, OnPlayFabError);
    }

    private void OnGetPlayerCombinedInforSuccess(GetPlayerCombinedInfoResult result)
    {
        OnLoadUserCombinedDataSuccessEvent?.Invoke(result.InfoResultPayload);
    }
    #endregion

    #region ACCOUNT_INFO

    public void GetAccountInfo(UnityAction<UserAccountInfo> onSuccess)
    {
        isBusy = true;
        Debug.Log("Getting user infor");
        var request = new GetAccountInfoRequest();
        OnGetUserAccountInforSuccessEvent = onSuccess;
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInforSuccess, OnPlayFabError);
    }

    private void OnGetAccountInforSuccess(GetAccountInfoResult result)
    {
        OnGetUserAccountInforSuccessEvent?.Invoke(result.AccountInfo);
        isBusy = false;
    }

    public void UpdateUserDisplayName(string value, UnityAction<string> onSuccess)
    {
        isBusy = true;
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = value
        };
        OnUpdateDisplayNameSuccessEvent = onSuccess;
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetDisplayNameSuccess, OnPlayFabError);
    }

    private void OnSetDisplayNameSuccess(UpdateUserTitleDisplayNameResult data)
    {
        OnUpdateDisplayNameSuccessEvent?.Invoke(data.DisplayName);
        isBusy = false;
    }

    #endregion

    #region STATISTICS

    public void SetPlayerStatistics(string name, int value, UnityAction onSuccess)
    {
        isBusy = true;
        OnUpdatePlayerStatisticSuccessEvent = onSuccess;
        var data = new List<StatisticUpdate>();
        data.Add(new StatisticUpdate { StatisticName = name, Value = value });

        SetStatistics(data);
    }

    public void SetPlayerStatistics(List<StatisticUpdate> data,UnityAction onSuccess)
    {
        isBusy = true;
        OnUpdatePlayerStatisticSuccessEvent = onSuccess;
        SetStatistics(data);
    }

    public void SetPlayerStatistics(List<StatisticUpdate> data)
    {
        isBusy = true;
        SetStatistics(data);
    }
    private void SetStatistics(List<StatisticUpdate> data)
    {
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = data;

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticSuccess, OnPlayFabError);
    }

    private void OnUpdateStatisticSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Update player statistic success");
        OnUpdatePlayerStatisticSuccessEvent?.Invoke();
        isBusy = false;
    }

    public void GetPlayerStatistic(UnityAction<List<StatisticValue>> onSuccess)
    {
        isBusy = true;
        OnGetPlayerStatisticSuccessEvent = onSuccess;
        Debug.Log("Getting user statistic");
        var request = new GetPlayerStatisticsRequest();

        PlayFabClientAPI.GetPlayerStatistics(request, OnGetStatisticSuccess, OnPlayFabError);
    }

    public void OnGetStatisticSuccess(GetPlayerStatisticsResult result)
    {
        OnGetPlayerStatisticSuccessEvent?.Invoke(result.Statistics);
        isBusy = false;
    }

    #endregion Statistics

    #region PLAYER_DATA

    public void GetUserData()
    {
        isBusy = true;
        var request = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData(request, OnGetUserDataSuccess, OnPlayFabError);
    }


    public void GetUserData(UnityAction<Dictionary<string, UserDataRecord>> onSuccess)
    {
        isBusy = true;
        OnGetUserDataSuccessEvent = onSuccess;
        var request = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData(request, OnGetUserDataSuccess, OnPlayFabError);
    }
    private void OnGetUserDataSuccess(GetUserDataResult result)
    {
        OnGetUserDataSuccessEvent?.Invoke(result.Data);

        isBusy = false;
    }
    public void UpdateUserData(Dictionary<string, string> data, UnityAction onSuccess)
    {
        isBusy = true;
        OnUpdateUserDataSuccessEvent = onSuccess;
        var request = new UpdateUserDataRequest();
        request.Data = data;

        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataSuccess, OnPlayFabError);
    }

    public void UpdateUserData(Dictionary<string, string> data)
    {
        isBusy = true;
        var request = new UpdateUserDataRequest();
        request.Data = data;

        PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataSuccess, OnPlayFabError);
    }

    private void OnUpdateUserDataSuccess(UpdateUserDataResult result)
    {
        isBusy = false;
        OnUpdateUserDataSuccessEvent?.Invoke();
    }

    #endregion PlayerData

    #region INVENTORY

    public void GetUserInventory(UnityAction onSuccess)
    {
        Debug.Log("Getting user inventory");
        isBusy = true;
        OnGetUserInventorySuccessEvent = onSuccess;

        var request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, OnGetUserInventorySuccess, OnPlayFabError);
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        OnGetUserInventorySuccessEvent?.Invoke();
        playerData.WriteVirtualCurrency(result.VirtualCurrency);

        isBusy = false;
    }

    public void SetUserInventory(string currency, int value)
    {
        isBusy = true;
        var request = new AddUserVirtualCurrencyRequest();
        request.Amount = value;
        request.VirtualCurrency = currency;

        PlayFabClientAPI.AddUserVirtualCurrency(request, SetUserVirtualCurrencySuccess, OnPlayFabError);
    }

    private void SetUserVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        playerData.WriteVirtualCurrency(result.VirtualCurrency, result.Balance);

        isBusy = false;
    }

    #endregion Inventory

    #region SAVE_USER_REWARD
    public void SaveRewardUserData(int goldAmount, int gemAmount)
    {
        StartCoroutine(CoSetUserData(goldAmount, gemAmount));
    }

    private IEnumerator CoSetUserData(int goldAmount, int gemAmount)
    {
        EventHandlerUI.ActiveLoading(true);

        List<StatisticUpdate> data = new List<StatisticUpdate>();
        data.Add(new StatisticUpdate { StatisticName = PrefsKeys.Trophy, Value = playerData.trophy });
        data.Add(new StatisticUpdate { StatisticName = PlayerPrefs.GetString(PrefsKeys.TrophyLocal), Value = playerData.trophy });
        
        SetPlayerStatistics(data);

         yield return new WaitUntil(IsAvailableApiCall); //Wait until get save user statistic success

        Dictionary<string, string> userData = new Dictionary<string, string>()
        {
            {PrefsKeys.Level, playerData.level.ToString() },
            {PrefsKeys.Experience, playerData.experience.ToString() },
            {PrefsKeys.TotalGames, playerData.totalGames.ToString()},
            {PrefsKeys.WinGames, playerData.winGames.ToString() },
            {PrefsKeys.MVP, playerData.mvp.ToString() },
            {PrefsKeys.Rampages, playerData.rampage.ToString() },
            {PrefsKeys.WinStreaks, playerData.winstreak.ToString() }
        };

        UpdateUserData(userData);

        yield return new WaitUntil(IsAvailableApiCall);

        SetUserInventory(PrefsKeys.Gold, goldAmount);

        yield return new WaitUntil(IsAvailableApiCall);

        if (gemAmount != 0)
        {
            SetUserInventory(PrefsKeys.Gem, gemAmount);
        }

        yield return new WaitUntil(IsAvailableApiCall);

    }
    #endregion


    private void OnPlayFabError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
        EventHandlerUI.OnShowPopUpEvent("Error", "Please check your internet connection!");

        isBusy = false;
    }

    private bool IsAvailableApiCall()
    {
        return isBusy;
    }
}