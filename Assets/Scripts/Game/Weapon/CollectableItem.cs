
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [HideInInspector]
    public ObjectSpawner spawner;

    [HideInInspector]
    public GameObject colliderObject;

    public ScriptableItem item;

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if(character!= null)
        {
            if (item.IsUseable(character))
            {
                item.Action(character, OnActionComplete);
            }
        }
    }

    public void OnActionComplete()
    {
        spawner.DestroyItem();
    }
}