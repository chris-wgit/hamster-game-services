using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;


public class PlayfabLeaderboard : MonoBehaviour
{
    [Header("Leaderboard content")]
    public GameObject listingPrefab;
    public Transform listingContainerGlobal;
    public Transform listingContainerLocal;

    [Header("Leaderboard UI")]
    public TextMeshProUGUI yourPositionText;
    public TextMeshProUGUI yourScoreText;
    public TextMeshProUGUI playerNameText;

    private bool globalInitialized;
    private bool localInitialized;

    public UITapViewController tabController;

    private void Start()
    {
        GetGlobalLeaderboard();
        GetLeaderboardAroundUser();
    }

    public void OnGetGlobalLeaderboardButtonClicked()
    {
        if (!globalInitialized)
        {
            GetGlobalLeaderboard();
        }
        GetLeaderboardAroundUser();
        tabController.SelectTabGroup(0);
    }

    public void OnGetLocalLeaderboardButtonClicked()
    {
        if (!localInitialized)
        {
            GetLocalLeaderboard();
        }
        GetLeaderboardAroundUserRegion();
        tabController.SelectTabGroup(1);
    }
    private void GetGlobalLeaderboard()
    {
        EventHandlerUI.ActiveLoading(true, "Loading");
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = PrefsKeys.Trophy, MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, (v)=>InstantiateLeaderboard(v, listingContainerGlobal), OnPlayfabError);
    }

    private void GetLocalLeaderboard()
    {
        EventHandlerUI.ActiveLoading(true, "Loading");
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = PlayerPrefs.GetString(PrefsKeys.TrophyLocal), MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, (v)=> InstantiateLeaderboard(v,listingContainerLocal), OnPlayfabError);
    }

    private void InstantiateLeaderboard(GetLeaderboardResult result, Transform parent)
    {
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            if (player.StatValue != 0)
            {
                GameObject tempListing = Instantiate(listingPrefab, parent);
                LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();

                SetupLeaderboardListing(player, LL);
            }
        }
        if(parent == listingContainerGlobal)
        {
            globalInitialized = true;
        }
        if (parent == listingContainerLocal)
        {
            localInitialized = true;
        }

        EventHandlerUI.ActiveLoading(false);
    }

    private void SetupLeaderboardListing(PlayerLeaderboardEntry player, LeaderboardListing LL)
    {
        int _position = player.Position + 1;

        LL.position.text = _position.ToString();

        LL.playerName.text = player.DisplayName;

        LL.playerTrophy.text = player.StatValue.ToString();

        switch (player.Position)
        {
            
            case 0:
                LL.goldMedal.SetActive(true);
                    break;

            case 1:
                LL.silverMedal.SetActive(true);
                    break;

            case 2:
                LL.bronzeMedal.SetActive(true);
                    break;
        }

    }

    private void ClearLeaderboardPanel()
    {
        foreach (Transform child in listingContainerGlobal)
        {
            Destroy(child.gameObject);
        }
    }

    private void GetLeaderboardAroundUser()
    {
        var request = new GetLeaderboardAroundPlayerRequest() { StatisticName = PrefsKeys.Trophy, MaxResultsCount = 1 };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundUserSuccess, OnPlayfabError);
    }

    private void GetLeaderboardAroundUserRegion()
    {
        var request = new GetLeaderboardAroundPlayerRequest() { StatisticName = PlayerPrefs.GetString(PrefsKeys.TrophyLocal), MaxResultsCount = 1 };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundUserSuccess, OnPlayfabError);
    }


    private void OnGetLeaderboardAroundUserSuccess(GetLeaderboardAroundPlayerResult result)
    {
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.StatValue);
            yourPositionText.text ="Your Rank: " + (player.Position + 1).ToString();
            yourScoreText.text =  player.StatValue.ToString();
            playerNameText.text = player.DisplayName;

        }

    }

    private void OnPlayfabError(PlayFabError obj)
    {
        EventHandlerUI.OnShowPopUpEvent("Error", "Check your conection!");
        Debug.LogError(obj.ToString());

    }
}

