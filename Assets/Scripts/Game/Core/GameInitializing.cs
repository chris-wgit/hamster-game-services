using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializing : MonoBehaviour
{
    public GameObject gameManagerObject;
    private void Awake()
    {
        if (GameInstance.Instance == null) Instantiate(gameManagerObject, Vector3.zero, Quaternion.identity);
    }
}
