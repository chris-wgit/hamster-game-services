using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentBuildOnly : MonoBehaviour
{

    void Start()
    {
        if (!Debug.isDebugBuild)
        {
            gameObject.SetActive(false);
        }
    }



}
