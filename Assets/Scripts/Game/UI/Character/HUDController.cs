using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDController : MonoBehaviour
{
    private Character character;

    public UITextBehaviour userName;

    public UIUnitHealthbarBehaviour healthBar;

    public SkillSliderBehaviour reloadUI;

    public UISpriteBehaviour positionSprite;

    public HealthTextFX healthTextFX;

    public Canvas canvasHUD;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
    }

    private void OnEnable()
    {
        character.OnCharacterInitialized += SetupHUD;
        character.ConditionState.OnStateChange += ToggleHUD;
    }

    private void OnDisable()
    {
        character.OnCharacterInitialized -= SetupHUD;
        character.ConditionState.OnStateChange -= ToggleHUD;

    }

    private void ToggleHUD(CharacterConditions state)
    {
        switch (state)
        {
            case CharacterConditions.Invulrnerable:
                canvasHUD.enabled = true;
                break;
            case CharacterConditions.Normal:
                break;
            case CharacterConditions.Disabled:
                break;
            case CharacterConditions.Dead:
                canvasHUD.enabled = false;
                break;
        }
    }

    private void SetupHUD(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Master:
                SetMasterCharacter();
                break;
            case CharacterType.Ally:
                SetFriendCharacter();
                break;
            case CharacterType.Enemy:
                SetEnemiesCharacter();
                break;
        }

        SetPlayerName(character.NickName);
    }

    public void SetMasterCharacter()
    {
        //reloadUI.Initializing();
        healthTextFX.gameObject.SetActive(true);
    }

    public void SetPlayerName(string name)
    {
        userName.SetText(name);
    }

    public void SetFriendCharacter()
    {
        healthBar.SetFriendlyHealthBar();
        reloadUI.gameObject.SetActive(false);
        positionSprite.SetAllySprite();
    }

    public void SetEnemiesCharacter()
    {
        healthBar.SetEnemyHealthBar();
        reloadUI.gameObject.SetActive(false);
        positionSprite.SetEnemySprite();
    }

}
