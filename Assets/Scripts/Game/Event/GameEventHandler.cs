using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventHandler
{
    public static Action<GameObject> OnMasterCharacterSet;

    public static Action<GameState> OnGameStateChanged;
    public static void SetMasterCharacter(GameObject character)
    {
        OnMasterCharacterSet?.Invoke(character);
    }

    public static void GameStateChangedEvent(GameState state)
    {
        OnGameStateChanged?.Invoke(state);
    }
}
