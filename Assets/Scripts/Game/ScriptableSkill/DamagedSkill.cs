using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class DamagedSkill : ScriptableSkill
{
    [Required]
    [BoxGroup("GameObject", order: 3), PreviewField(80)]
    public GameObject damagedObject;
    [BoxGroup("Statistic")]
    public int damage;
    [BoxGroup("Collider",order:5)]
    public bool despawnOnColliderCharacter;
    [BoxGroup("Collider")]
    public bool despawnOnColliderEnvironment;

    public override Vector3 DestinationFromInput(Vector2 input, Transform normalizedTransform)
    {
        Vector3 destination = new Vector3(input.x, 0, input.y);
        destination = destination.z * normalizedTransform.forward + destination.x * normalizedTransform.right;
        destination.y = 0f;
        return destination;
    }
}
