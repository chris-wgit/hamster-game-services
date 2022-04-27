using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int targetFrameRate = 30;
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }


}
