using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : Singleton<UIGameManager>
{
    public GameplayController _gameController;

    public GameObject gameplayUI;
    public GameObject respawnUI;
    public GameObject gameOverUI;

    public UITextBehaviour timeCountText;
    public float timeLeft;

    private bool isGameStarted;
    private void OnEnable()
    {
        EventHandlerGameplay.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        EventHandlerGameplay.OnGameStateChanged -= OnGameStateChanged;
    }
    private void Start()
    {
        ToggleUI(gameOverUI, false);
        timeLeft = GameInstance.Instance._GameMode.gameTime;
    }
    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.WaitConnection:
               
                break;
            case GameState.Playing:
                if (!isGameStarted) isGameStarted = true;
                SetGamePlayUI();
                break;
            case GameState.Respawn:
                SetRespawnGameUI();
                break;
            case GameState.GameOver:
                SetGameOverUI();
                break;
            default:
                break;
        }
    }

    public void AddExtraTime(int bonusTime)
    {
        timeLeft += bonusTime;
    }
    private void Update()
    {
        if (isGameStarted && timeLeft>0)
        {
            timeLeft -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeLeft);
            timeCountText.SetText(time.ToString("mm':'ss"));

        }

        if (timeLeft < 0)
        {
            timeLeft = 0;
            _gameController.CallTimeOut();
        }
    }

    private void SetRespawnGameUI()
    {
        ToggleUI(respawnUI, true);
        ToggleUI(gameplayUI, false);
    }

    private void SetGamePlayUI()
    {
        ToggleUI(respawnUI, false);
        ToggleUI(gameplayUI, true);
    }

    private void SetGameOverUI()
    {
        ToggleUI(gameplayUI, false);
        ToggleUI(respawnUI, false);
        ToggleUI(gameOverUI, true);
    }

    private void ToggleUI(GameObject UI, bool value)
    {
        UI.SetActive(value);
    }

}
