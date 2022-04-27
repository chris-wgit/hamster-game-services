using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiddenShader : MonoBehaviour
{
    public FadeShader fadeShader;

    public bool CanHide()
    {
        return !fadeShader.isTransparent;
    }
    [Button(ButtonSizes.Large)]
    public void DoAnimation()
    {
        fadeShader.gameObject.transform.DOShakeScale(.2f, .2f, 3, 90);
    }

}
