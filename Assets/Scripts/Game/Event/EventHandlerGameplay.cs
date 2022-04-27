using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandlerGameplay
{
    public static Action<GameObject> OnMasterCharacterSet;

    public static Action<GameState> OnGameStateChanged;

    public static Action<Player> OnMasterCharacterDead;
    public static void SetMasterCharacter(GameObject character)
    {
        OnMasterCharacterSet?.Invoke(character);
    }

    public static void GameStateChangedEvent(GameState state)
    {
        OnGameStateChanged?.Invoke(state);
    }

    public static void SetMasterCharacterDead(Player killer)
    {
        OnMasterCharacterDead?.Invoke(killer);
    }
}
