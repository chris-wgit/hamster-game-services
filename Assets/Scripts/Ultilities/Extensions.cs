using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    public static int SecondFromFloat(float timeValue)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(timeValue);
        return time.Seconds;
    }
}
