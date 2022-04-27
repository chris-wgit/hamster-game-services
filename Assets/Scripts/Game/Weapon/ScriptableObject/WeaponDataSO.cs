using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponDataSO : ScriptableObject
{
    [BoxGroup("Basic Infor")]
    public string id;

    [BoxGroup("Basic Infor")]
    public string weaponName;

    [BoxGroup("Basic Infor")]
    [Multiline]
    public string description;

    [BoxGroup("Visual")]
    [PreviewField(75)]
    public Sprite icon;

    [BoxGroup("Visual")]
    [PreviewField(75)]
    public GameObject weaponPrefab_R;
    [BoxGroup("Visual")]
    [PreviewField(75)]
    public GameObject weaponPrefab_L;

    [BoxGroup("Visual")]
    public AnimatorOverrideController overrideAnimator;

    [BoxGroup("Physic")]
    [Tooltip("Weight affect character movement speed")]
    [ShowInInspector]
    private int weight;

    [BoxGroup("Physic")]
    public float speedAffect { get { return weight * 0.05f; } }

    [BoxGroup("Skill")]
    public DamagedSkill weaponSkill;
    [BoxGroup("Skill")]
    public ScriptableSkill weaponUltimate;

    public ShootPos shootPosition;

    public virtual void EquipWeapon(Transform handerL, Transform handerR, Animator animator)
    {
        if (weaponPrefab_L != null)
        {
            Instantiate(weaponPrefab_L, handerL);
        }
        if (weaponPrefab_R != null)
        {
            Instantiate(weaponPrefab_R, handerR);
        }
        if (overrideAnimator != null) animator.runtimeAnimatorController = overrideAnimator;
    }

    public virtual void WeaponAttack(Character owner, LocalTransform transform)
    {
        
    }

}

public enum ShootPos
{
    None,
    LeftHandler,
    RightHandler
}
