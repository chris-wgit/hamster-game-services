using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabGetPlayerLocation : MonoBehaviour
{

    [ShowInInspector]
    public List<LocationModel> locationInfor;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Country")) 
        {
            PlayfabData.GetPlayerProfile(SaveUserLocation);
        }
    }

    private void SaveUserLocation(GetPlayerProfileResult result)
    {
        locationInfor = result.PlayerProfile.Locations;
        Debug.Log("Saving player Location");
        if (result.PlayerProfile.Locations != null)
        {
            string countryCode = result.PlayerProfile.Locations[0].CountryCode.ToString();
            PlayerPrefs.SetString("Country", countryCode);
            PlayerPrefs.SetString(PrefsKeys.TrophyLocal, $"Trophy{countryCode}");
            Debug.Log("CountryCode is " + countryCode);
        }
        else
        {
            PlayerPrefs.SetString("Country", "US");
            PlayerPrefs.SetString(PrefsKeys.TrophyLocal, $"TrophyUS");
            Debug.Log("CountryCode is US");
        }

    }
}
