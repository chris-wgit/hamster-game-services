using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(fileName = "Game Setting", menuName = "Data/Game Setting")]
public class GameSetingSO : ScriptableObject
{
    public Action<bool> OnMusicChanged;

    [BoxGroup("Game Setting")]
    public bool music { get; private set; }
    [BoxGroup("Game Setting")]
    public bool sound { get; private set; }
    [BoxGroup("Game Setting")]
    public bool vibration { get; private set; }
    [BoxGroup("Game Setting")]
    public bool notification { get; private set; }

    private void Awake()
    {
        Initialization();
    }

    public void Initialization()
    {
        music = IntToBool(PlayerPrefs.GetInt("Music", 1));
        sound = IntToBool(PlayerPrefs.GetInt("Sound", 1));
        vibration = IntToBool(PlayerPrefs.GetInt("Vibration", 1));
        notification = IntToBool(PlayerPrefs.GetInt("Notification", 1));
    }

    public void SetMusic(bool value)
    {
        music = value;
        PlayerPrefs.SetInt("Music",BoolToInt(value));

        OnMusicChanged?.Invoke(value);
    }

    public void SetSound(bool value)
    {
        sound = value;
        PlayerPrefs.SetInt("Sound", BoolToInt(value));
    }

    public void SetVibration(bool value)
    {
        vibration = value;
        PlayerPrefs.SetInt("Vibration", BoolToInt(value));
    }

    public void SetNotification(bool value)
    {
        notification = value;
        PlayerPrefs.SetInt("Notification", BoolToInt(value));
    }


    public static int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }

    public static bool IntToBool(int value)
    {
        return value == 1;
    }
}

