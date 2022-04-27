using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterLibrary : MonoBehaviour
{
    public CharacterLibrarySO _CharacterList;

    public GameObject characterSlotPrefab;
    public Transform characterSlotParent;

    public GridLayoutGroup layoutGroup;
    public ContentSizeFitter contentSize;
    // Start is called before the first frame update
    void Start()
    {
        SetCharacterList();

    }

    public void SetCharacterList()
    {
        foreach (var item in _CharacterList.CharacterList)
        {
            GameObject characterSlot = Instantiate(characterSlotPrefab, characterSlotParent);
            CharacterSlotUI characterInfor = characterSlot.GetComponent<CharacterSlotUI>();
            characterInfor._CharacterData = item.Value;
            characterInfor.SetCharacterUI();
        }

    }

    private void OnDisable()
    {

        contentSize.enabled = false;

        layoutGroup.enabled = false;
    }

}

