using UnityEngine;
using System.Collections;
using Photon.Pun;
using System;

public class GameUIController : Singleton<GameUIController>
{

    public UISkillButton[] skillButtonUI;

    public Canvas controllerCanvas;

    public GameObject deadCanvas;

    public VoidEventChannelSO _OnGameOver;
    public VoidEventChannelSO _OnGameStateChanged;

    private void OnEnable()
    {
        _OnGameOver.OnEventRaised += GameOverUI;
        _OnGameStateChanged.OnEventRaised += OnGameStateChanged;
    }
    private void OnDisable()
    {
        _OnGameOver.OnEventRaised -= GameOverUI;
        _OnGameStateChanged.OnEventRaised -= OnGameStateChanged;

    }

    private void OnGameStateChanged()
    {
        switch (GameplayController.instance._GameState.CurrentState)
        {
            case GameState.WaitConnection:
                WarmUpGameUI();
                break;
            case GameState.Playing:
                GamePlayUI();
                break;
            case GameState.Respawn:
                DeadGameUI();
                break;
            case GameState.GameOver:
                GameOverUI();
                break;
        }
    }

    void WarmUpGameUI()
    {
        ToggleGameplayUI(false);
    }
    public void DeadGameUI()
    {
        ToggleGameplayUI(false);
        deadCanvas.SetActive(true);
    }

    public void GamePlayUI()
    {
        ToggleGameplayUI(true);
        deadCanvas.SetActive(false);
    }

    public void GameOverUI()
    {
        ToggleGameplayUI(false);
    }

    void ToggleGameplayUI(bool value)
    {
        controllerCanvas.enabled = value;
    }


}
