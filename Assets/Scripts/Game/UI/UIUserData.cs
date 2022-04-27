using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUserData : MonoBehaviour
{

    private TextMeshProUGUI inforText;

    private PlayerDataSO data;
    public PlayerDataEnum infor;

    private void Awake()
    {
        inforText = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        data.OnUserDataUpdated -= SetText;
    }
    private void Start()
    {
        data = GameInstance.Instance._PlayerData;
        data.OnUserDataUpdated += SetText;
        SetText();
    }

    private void SetText()
    {
        switch (infor)
        {
            case PlayerDataEnum.Username:
                inforText.text = data.displayName;
                break;
            case PlayerDataEnum.Experience:
                inforText.text = data.experience.ToString();
                break;
            case PlayerDataEnum.Trophy:
                inforText.text = data.trophy.ToString();
                break;
            case PlayerDataEnum.MVP:
                inforText.text = data.mvp.ToString();
                break;
            case PlayerDataEnum.WinRate:
                inforText.text = data.winRate.ToString() + "%";
                break;
            case PlayerDataEnum.TotalGames:
                inforText.text = data.totalGames.ToString();
                break;
            case PlayerDataEnum.WinGames:
                inforText.text = data.winGames.ToString();
                break;
            case PlayerDataEnum.Rampange:
                inforText.text = data.rampage.ToString();
                break;
            case PlayerDataEnum.WinStreaks:
                inforText.text = data.winstreak.ToString();
                break;
            case PlayerDataEnum.Level:
                inforText.text = data.level.ToString();
                break;
        }
    }
}
