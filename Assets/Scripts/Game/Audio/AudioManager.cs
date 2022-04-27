using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameSetingSO _GameSetting;

    public GameObject fxAudioPrefab;

    public AudioSource musicAudioSource;

    public AudioSource soundAudioSource;


    private void OnEnable()
    {
        EventHandlerFX.On3DAudioEvent += PlaySoundFX;
        EventHandlerFX.OnSoundFX += PlaySound;
        EventHandlerFX.OnMusicFX += PlayMusic;

        _GameSetting.OnMusicChanged += UpdateMusicState;

    }

    private void OnDisable()
    {
        EventHandlerFX.OnMusicFX -= PlayMusic;
        EventHandlerFX.On3DAudioEvent -= PlaySoundFX;
        EventHandlerFX.OnSoundFX -= PlaySound;

        _GameSetting.OnMusicChanged -= UpdateMusicState;

    }

    private void UpdateMusicState(bool value)
    {
        
    }

    public void PlayMusic(AudioClip clip)
    {
        if (!_GameSetting.music) return;
        if (clip == null) return;

        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        if (!_GameSetting.sound) return;
        if (clip == null) return;

        soundAudioSource.clip = clip;
        soundAudioSource.Play();
    }


    public void PlaySoundFX(AudioClip clip, Vector3 position)
    {
        if (!_GameSetting.sound) return;
        //cancel execution if clip wasn't set
        if (clip == null) return;
        //calculate random pitch in the range around 1, up or down
        //pitch = Random.Range(1 - pitch, 1 + pitch);

        //activate new audio gameobject from pool
        GameObject audioObj = PoolManager.Spawn(fxAudioPrefab, position, Quaternion.identity);
        //get audio source for later use
        AudioSource source = audioObj.GetComponent<AudioSource>();

        //assign properties, play clip
        source.clip = clip;
        //source.pitch = pitch;
        source.Play();

        //deactivate audio gameobject when the clip stops playing
        PoolManager.Despawn(audioObj, clip.length);
    }
}
