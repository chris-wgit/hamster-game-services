using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEditUsername : MonoBehaviour
{
    public PlayerDataSO playerData;
    private TextMeshProUGUI nameText;

    private void Awake()
    {
        nameText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        playerData._OnUserDataChanged += SetUserName;
    }
    private void OnDisable()
    {
        playerData._OnUserDataChanged -= SetUserName;
    }


    private void Start()
    {
        SetUserName();
    }

    private void SetUserName()
    {
        nameText.text = playerData.displayName;
    }
}
