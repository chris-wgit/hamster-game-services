using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities.Inspector;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UIGameResultBehaviour : MonoBehaviour
{
    public GameplaySO _Gameplay;
    public PlayerDataSO _PlayerData;

    public RewardDataSO rewardData;

    public TextMeshProUGUI trophyText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI expText;

    public SceneField mainMenuScene;
    private void Start()
    {
        SetupResultDisplay();
    }
    void SetupResultDisplay()
    {
        trophyText.text = rewardData.trophy.ToString();
        goldText.text = rewardData.gold.ToString();
        expText.text = rewardData.experience.ToString();
    }
    public void OnClaimButtonClicked()
    {
        SavePlayerReward();
    }

    void SavePlayerReward()
    {
        EventHandlerUI.ActiveLoading(true);
        StartCoroutine(PlayfabData.UpdateUserReward(rewardData, OnUpdateUserDataSuccess));
    }

    public void OnUpdateUserDataSuccess()
    {

        EventHandlerUI.ActiveLoading(false);

        EventHandlerScene.LoadScene(mainMenuScene, LoadSceneMode.Single);

    }


}
