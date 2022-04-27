using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "CharacterLibrary", menuName = "GameData/CharacterLibrary")]
public class CharacterLibrarySO : ScriptableObject
{
    [ShowInInspector]
    public Dictionary<int, CharacterDataSO> CharacterList;
    //public Addressables locations;
    
    private void OnEnable()
    {
        Initialization();
    }

    private void Initialization()
    {
        CharacterList = new Dictionary<int, CharacterDataSO>();

        LoadCharacterData();
    }

    public void LoadCharacterData()
    {
        var characters = Resources.LoadAll<CharacterDataSO>("CharacterData");
        foreach (var item in characters)
        {
            CharacterList.Add(item.id, item);
        }
    }

    //async void LoadCharacterData()
    //{

    //    AsyncOperationHandle<IList<CharacterDataSO>> handle =
    //        Addressables.LoadAssetsAsync<CharacterDataSO>("CharacterDataSO", obj => CharacterList.Add(obj.id, obj));
    //    await handle.Task;
    //}

    public CharacterDataSO GetCharacter(int characterID)
    {
        CharacterDataSO character;
        CharacterList.TryGetValue(characterID, out character);
        if (character != null) return character;
        return CharacterList.Values.ElementAt(1);
    }



}