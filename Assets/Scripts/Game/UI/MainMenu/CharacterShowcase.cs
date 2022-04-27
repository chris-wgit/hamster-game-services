using DesertFoxTeam;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterShowcase : MonoBehaviour
{
    public CharacterLibrarySO _CharacterLibrary;

    public PlayerDataSO _PlayerData;

    public Transform characterParent;

    [HideInInspector]
    public GameObject currentCharacterObject;
    public CharacterEquipWeapon currentEquipWeapon;

    private void OnEnable()
    {
        EventHandlerUI.OnChangeCharacterEvent += UpdateCurrentCharacter;
        EventHandlerUI.OnSwapCharacterEvent += UpdateSelectedCharacter;
       EventHandlerUI.OnEquipWeaponEvent += UpdateCurrentWeapon;
    }
    private void OnDisable()
    {
        EventHandlerUI.OnChangeCharacterEvent -= UpdateCurrentCharacter;
        EventHandlerUI.OnSwapCharacterEvent -= UpdateSelectedCharacter;
        EventHandlerUI.OnEquipWeaponEvent -= UpdateCurrentWeapon;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrentCharacter();
    }
    private void UpdateCurrentCharacter()
    {
        UpdateSelectedCharacter(_CharacterLibrary.CharacterList[_PlayerData.currentCharacter]);
    }
    void UpdateSelectedCharacter(CharacterDataSO characterData)
    {
        foreach (Transform child in characterParent)
        {
            Destroy(child.gameObject);
        }
        currentCharacterObject = Instantiate(characterData.characterMesh, characterParent);
        currentEquipWeapon = currentCharacterObject.GetComponent<CharacterEquipWeapon>();

        //UpdateCurrentWeapon();
    }

    void UpdateCurrentWeapon()
    {
        currentEquipWeapon.SetWeapon(_PlayerData.weaponID);

    }
}

