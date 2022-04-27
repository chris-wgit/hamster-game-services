using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    public GameSetingSO _GameSetting;

    public UISwitchButton musicButton;
    public UISwitchButton soundButton;

    public UISwitchButton vibranceButton;
    public UISwitchButton notificationButton;

    private void Start()
    {
        SetupButtonUI();
    }

    public void SetupButtonUI()
    {
        musicButton.SetSwithState(_GameSetting.music);
        soundButton.SetSwithState(_GameSetting.sound);
        vibranceButton.SetSwithState(_GameSetting.vibration);
        notificationButton.SetSwithState(_GameSetting.notification);
    }
    public void OnMusicButtonClicked()
    {
        _GameSetting.SetMusic(musicButton.isActive);
    }

    public void OnSoundButtonClicked()
    {
        _GameSetting.SetSound(soundButton.isActive);
    }

    public void OnVibrationButtonClicked()
    {
        _GameSetting.SetVibration(vibranceButton.isActive);
    }

    public void OnNotificationButtonClicked()
    {
        _GameSetting.SetNotification(notificationButton.isActive);
    }
}
