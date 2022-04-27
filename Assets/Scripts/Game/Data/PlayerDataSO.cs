using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "UserData/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    #region Data
    [BoxGroup("Account Infor")]
    public string playfabID;

    [BoxGroup("Account Infor")]
    public string displayName;

    [BoxGroup("User Statistic")]
    public int trophy;

    [BoxGroup("UserData")]
    public int level = 1;

    [BoxGroup("UserData")]
    public int experience;

    [BoxGroup("UserData")]
    public int mvp;

    [BoxGroup("UserData")]
    public int totalGames;

    [BoxGroup("UserData")]
    public int winGames;

    [BoxGroup("UserData")]
    public int winRate{ get { return totalGames == 0 ? 0 : (int)(winGames * 100 / totalGames); } }

    [BoxGroup("UserData")]
    public int rampage;

    [BoxGroup("UserData")]
    public int winstreak;

    [BoxGroup("Virtual Currentcy")]
    public int gold;

    [BoxGroup("Virtual Currentcy")]
    public int gem;

    [BoxGroup("User Setting")]
    public int currentCharacter = 0;
    [BoxGroup("User Setting")]
    public string weaponID;

    public Action _OnUserDataChanged;
    public Action _OnGoldChanged;
    public Action _OnGemChanged;

    public Action _OnUsernameChanged;

    [BoxGroup("Player Experience")]
    [ShowInInspector]
    public int TotalExpRequirement { get { return PlayerExperience._maxExp.Get(level); } }

    [BoxGroup("Player Experience")]
    [ShowInInspector]
    public int ExpRequirement { get { return TotalExpRequirement - experience; } }

    [Button(ButtonSizes.Large)]
    public void AddExperience(int value)
    {
        experience += value;
        if (experience >= TotalExpRequirement)
        {
            experience -= TotalExpRequirement;
            level++;
        }
    }

    #endregion

    #region Event
    public Action OnStatisticChanged;
    public Action OnUserDataUpdated;
    public Action<ModifyUserVirtualCurrencyResult> OnVirtualCurrencyChanged;
    #endregion

    public void WriteCombinedData(GetPlayerCombinedInfoResultPayload resultPayload)
    {
        EventHandlerDebug.ShowLog("Write combined data");
        WriteAccountInfo(resultPayload.AccountInfo);
        WriteStatistics(resultPayload.PlayerStatistics);
        WriteUserData(resultPayload.UserData);
        WriteVirtualCurrency(resultPayload.UserVirtualCurrency);
        EventHandlerDebug.ShowLog("Write combined data success");
    }

    public void WriteAccountInfo(UserAccountInfo data)
    {
        displayName = data.TitleInfo.DisplayName;
        _OnUserDataChanged?.Invoke();
    }

    public void WriteUsername(string name)
    {
        displayName = name;
        _OnUserDataChanged?.Invoke();
    }

    public void WritePlayerProfiles(PlayerProfileModel profile)
    {
        displayName = profile.DisplayName;
    }

    public void WriteStatistics(List<StatisticValue> statistics)
    {
        foreach (var statistic in statistics)
        {
            switch (statistic.StatisticName)
            {
                case PrefsKeys.Trophy:
                    trophy = statistic.Value;
                    break;
            }
        }
        OnStatisticChanged?.Invoke();
    }

    public void WriteStatistics(List<StatisticUpdate> statistics)
    {
        foreach (var statistic in statistics)
        {
            switch (statistic.StatisticName)
            {
                case PrefsKeys.Trophy:
                    trophy = statistic.Value;
                    break;
            }
        }
        OnStatisticChanged?.Invoke();
    }

    public void WriteUserData(Dictionary<string, string> Data)
    {
        if (Data != null)
        {
            if (Data.ContainsKey(PrefsKeys.Level)) level = Int32.Parse(Data[PrefsKeys.Level]);
            if (Data.ContainsKey(PrefsKeys.Experience)) experience = Int32.Parse(Data[PrefsKeys.Experience]);
            if (Data.ContainsKey(PrefsKeys.MVP)) mvp = Int32.Parse(Data[PrefsKeys.MVP]);
            if (Data.ContainsKey(PrefsKeys.TotalGames)) totalGames = Int32.Parse(Data[PrefsKeys.TotalGames]);
            if (Data.ContainsKey(PrefsKeys.WinGames)) winGames = Int32.Parse(Data[PrefsKeys.WinGames]);
            if (Data.ContainsKey(PrefsKeys.Rampages)) rampage = Int32.Parse(Data[PrefsKeys.Rampages]);
            if (Data.ContainsKey(PrefsKeys.WinStreaks)) winstreak = Int32.Parse(Data[PrefsKeys.WinStreaks]);

            if (Data.ContainsKey(PrefsKeys.CurrentCharacter)) currentCharacter = Int32.Parse(Data[PrefsKeys.CurrentCharacter]);
            if (Data.ContainsKey(PrefsKeys.CurrentWeapon)) weaponID = (Data[PrefsKeys.CurrentWeapon]); else weaponID = "shotgun";
        }
        OnUserDataUpdated?.Invoke();
    }

    public void WriteUserData(Dictionary<string, UserDataRecord> Data)
    {
        if (Data != null)
        {
            if (Data.ContainsKey(PrefsKeys.Level)) level = Int32.Parse(Data[PrefsKeys.Level].Value);
            if (Data.ContainsKey(PrefsKeys.Experience)) experience = Int32.Parse(Data[PrefsKeys.Experience].Value);
            if (Data.ContainsKey(PrefsKeys.MVP)) mvp = Int32.Parse(Data[PrefsKeys.MVP].Value);
            if (Data.ContainsKey(PrefsKeys.TotalGames)) totalGames = Int32.Parse(Data[PrefsKeys.TotalGames].Value);
            if (Data.ContainsKey(PrefsKeys.WinGames)) winGames = Int32.Parse(Data[PrefsKeys.WinGames].Value);
            if (Data.ContainsKey(PrefsKeys.Rampages)) rampage = Int32.Parse(Data[PrefsKeys.Rampages].Value);
            if (Data.ContainsKey(PrefsKeys.WinStreaks)) winstreak = Int32.Parse(Data[PrefsKeys.WinStreaks].Value);

            if (Data.ContainsKey(PrefsKeys.CurrentCharacter)) currentCharacter = Int32.Parse(Data[PrefsKeys.CurrentCharacter].Value);
            if (Data.ContainsKey(PrefsKeys.CurrentWeapon)) weaponID = (Data[PrefsKeys.CurrentWeapon].Value); else weaponID = "shotgun";
        }
        OnUserDataUpdated?.Invoke();
    }

    public void WriteVirtualCurrency(Dictionary<string,int> virtualCurrency)
    {
        if (virtualCurrency == null) return;
        if (virtualCurrency.ContainsKey("GD")) gold = virtualCurrency["GD"];
        if (virtualCurrency.ContainsKey("GM")) gem = virtualCurrency["GM"];
    }

    public void WriteVirtualCurrency(string currency, int value)
    {
        switch (currency)
        {
            case PrefsKeys.Gold:
                gold = value;
                break;
            case PrefsKeys.Gem:
                gem = value;
                break;
        }
    }

    public string TrophyLoccal { get { return PlayerPrefs.GetString(PrefsKeys.TrophyLocal); } }
}

public enum PlayerDataEnum
{
    Username,
    Experience,
    Trophy,
    MVP,
    WinRate,
    TotalGames,
    WinGames,
    Rampange,
    WinStreaks,
    Level
}