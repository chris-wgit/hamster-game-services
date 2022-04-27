using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; protected set; }

    public PlayerDataSO _PlayerData;

    public GameStatisticSO _GameStatistic;

    public CharacterLibrarySO CharacterLibrary;

    public WeaponLibrarySO WeaponLibrary;

    public GameplaySO _GameMode;


    public virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Application.runInBackground = true;
    }

    public void SetGameMode(GameplaySO gameMode)
    {
        _GameMode = gameMode;
    }
}
