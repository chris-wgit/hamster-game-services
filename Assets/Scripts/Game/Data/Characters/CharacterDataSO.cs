using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "GameData/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [Tooltip("Character order in Character Library")]
    public int id;

    // The name of the Character
    [BoxGroup("Basic Infor")]
    public string characterName;

    [BoxGroup("Basic Infor")]
    [Multiline]
    public string description;

    [BoxGroup("Character Visual")]
    [PreviewField(75)]
    public Sprite Icon;

    [BoxGroup("Character Visual")]
    [PreviewField(75)]
    public GameObject characterPrefab;

    [BoxGroup("Character Visual")]
    [PreviewField(75)]
    public GameObject characterMesh;

    [BoxGroup("Character Data")]
    public int health = 3000;

    [BoxGroup("Character Data")]
    [SerializeField]
    private int movementSpeed = 20;

    [BoxGroup("Character Data")]
    public float MovementSpeed { get { return (float)(movementSpeed*0.1) - WeaponAffected; } }

    [BoxGroup("Weapon Data")]
    public WeaponDataSO weaponData;

    [BoxGroup("Weapon Data")]
    public WeaponLibrarySO weaponLibrary;

    [BoxGroup("Skill Data")]
    public ScriptableSkill additionSkill;

    [BoxGroup("VFX")]
    public GameObject OnDespawnFX;

    public void EquipWeapon(string weaponID)
    {
        weaponData = weaponLibrary.GetWeapon(weaponID);
    }

    private float WeaponAffected { get { return weaponData != null ? weaponData.speedAffect : 0; } }
}