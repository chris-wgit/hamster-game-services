using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFrameRate : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        InvokeRepeating("UpdateFrameRate", 1, 1);
    }

    void UpdateFrameRate()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + (int)fps;
    }

}
