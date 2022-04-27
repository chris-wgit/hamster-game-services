using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUIController : MonoBehaviour
{
    private CooldownTimer cooldown;

    public UITextBehaviour cooddownText;

    private void Awake()
    {
        cooldown = GameplayController.instance.respawnCooldown;
    }

    private void OnEnable()
    {
        cooldown.OnTimeRemainingChanged += UpdateTextValue;
    }
    private void OnDisable()
    {
        cooldown.OnTimeRemainingChanged -= UpdateTextValue;
    }

    private void UpdateTextValue(float value)
    {
        cooddownText.SetText(((int)value).ToString());
    }
}
