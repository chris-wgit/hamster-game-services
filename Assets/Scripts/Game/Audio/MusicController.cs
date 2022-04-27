using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip clip;

    void Start()
    {
        EventHandlerFX.PlayMusic(clip)
;    }

}
