using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;

public static class PlayfabData
{
    public static Action<GetPlayerCombinedInfoResultPayload> OnGetCombinedDataSuccess;
    public static Action OnUpdateUserDataSuccess;

    private static PlayerDataSO playerData { get { return GameInstance.Instance._PlayerData; } }
    public static void LoadUserCombinedData(Action<GetPlayerCombinedInfoResultPayload> onSuccess)
    {
        GetPlayerCombinedInfoRequestParams requestParams = new GetPlayerCombinedInfoRequestParams();
        requestParams.GetUserAccountInfo = true;
        requestParams.GetPlayerStatistics = true;
        requestParams.GetUserData = true;
        requestParams.GetUserInventory = true;

        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest() { InfoRequestParameters = requestParams };

        PlayFabClientAPI.GetPlayerCombinedInfo(request, 
            (e) => {
                playerData.WriteCombinedData(e.InfoResultPayload);
                onSuccess?.Invoke(e.InfoResultPayload);
            }, 
            OnPlayfabError);
    }

    public static void UpdateUserDisplayName(string name, Action<string> onSuccess)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, 
            (e) =>
            {
                playerData.WriteUsername(e.DisplayName);
                onSuccess?.Invoke(e.DisplayName); }, 
            OnPlayfabError);
    }

    public static void GetUserData(Action<Dictionary<string, UserDataRecord>> onSuccess)
    {
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request, (result) => onSuccess?.Invoke(result.Data), OnPlayfabError);
    }

    public static void UpdateUserData(Dictionary<string, string> data, Action onSuccess)
    {
        var request = new UpdateUserDataRequest();
        request.Data = data;

        PlayFabClientAPI.UpdateUserData(request, (success) => {
            playerData.WriteUserData(data);
            onSuccess?.Invoke();
        } ,
            OnPlayfabError);
    }

    public static void GetPlayerProfile(Action<GetPlayerProfileResult> onSuccess)
    {
        GetPlayerProfileRequest request = new GetPlayerProfileRequest();
        PlayFabClientAPI.GetPlayerProfile(request, (p) => {
            playerData.WritePlayerProfiles(p.PlayerProfile);
            onSuccess?.Invoke(p);
        }, OnPlayfabError);
    }

    public static void GetPlayerStatistic(Action<List<StatisticValue>> onSuccess)
    {
        var request = new GetPlayerStatisticsRequest();
        PlayFabClientAPI.GetPlayerStatistics(request, (p) => {
            playerData.WriteStatistics(p.Statistics);
            onSuccess?.Invoke(p.Statistics);
        }, OnPlayfabError);
    }

    public static void UpdatePlayerStatistics(List<StatisticUpdate> data, Action onSuccess)
    {
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = data;
        PlayFabClientAPI.UpdatePlayerStatistics(request, (success) => {
            playerData.WriteStatistics(data);
            onSuccess?.Invoke();
        } , OnPlayfabError);
    }

    public static void GetUserInventory(Action<GetUserInventoryResult> onSuccess)
    {
        var request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, (success) => {
            playerData.WriteVirtualCurrency(success.VirtualCurrency);
            onSuccess?.Invoke(success);
        }, OnPlayfabError);
    }

    public static void SetUserVirtualCurrency(string currency, int value, Action<ModifyUserVirtualCurrencyResult> onSuccess)
    {
        var request = new AddUserVirtualCurrencyRequest();
        request.Amount = value;
        request.VirtualCurrency = currency;

        PlayFabClientAPI.AddUserVirtualCurrency(request, (success)=> {
            playerData.WriteVirtualCurrency(success.VirtualCurrency, success.Balance);
            onSuccess?.Invoke(success);
        } , OnPlayfabError);
    }


    public static IEnumerator UpdateUserReward(RewardDataSO rewardData, Action onUpdateSuccess)
    {
        bool isBusy = true;
        playerData.trophy += rewardData.trophy;
        if (playerData.trophy < 0) playerData.trophy = 0;
        List<StatisticUpdate> statistic = new List<StatisticUpdate>();
        statistic.Add(new StatisticUpdate
        {
            StatisticName = PrefsKeys.Trophy,
            Value = playerData.trophy
        });
        statistic.Add(new StatisticUpdate
        {
            StatisticName = playerData.TrophyLoccal,
            Value = playerData.trophy
        });
        PlayfabData.UpdatePlayerStatistics(statistic, () => isBusy = false);
        while (isBusy) yield return null;
        isBusy = true;

        Dictionary<string, string> userData = new Dictionary<string, string>();

        playerData.totalGames++;
        userData.Add(PrefsKeys.TotalGames, playerData.totalGames.ToString());

        userData.Add(PrefsKeys.WinGames, (playerData.winGames += rewardData.winGames).ToString());
        if (rewardData.winGames > 0)
        {
            playerData.winstreak++;
        }
        else
        {
            playerData.winstreak = 0;
        }
        userData.Add(PrefsKeys.WinStreaks, playerData.winstreak.ToString());
        PlayerExpData newExp = PlayerExperience.GetPlayerExpData(playerData.level, playerData.experience += rewardData.experience);
        userData.Add(PrefsKeys.Experience, newExp.experience.ToString());
        userData.Add(PrefsKeys.Level, newExp.level.ToString());

        UpdateUserData(userData, () => isBusy = false);
        while (isBusy) yield return null;

        isBusy = true;

        SetUserVirtualCurrency(PrefsKeys.Gold, rewardData.gold, OnVirtualCurrencyChanged => isBusy = false);

        while (isBusy) yield return null;

        onUpdateSuccess?.Invoke();
    }

    private static void OnPlayfabError(PlayFabError error)
    {

    }


}
