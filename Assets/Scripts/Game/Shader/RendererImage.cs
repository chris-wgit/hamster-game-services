using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class RendererImage : MonoBehaviour, IShaderFX
{
    protected CharacterRenderer renderManager;
    protected Image image;

    protected virtual void Awake()
    {
        image = GetComponentInParent<Image>();
        renderManager = GetComponentInParent<CharacterRenderer>();
        if (renderManager != null) renderManager.AddObjectToRenderer(this);
    }
    private void OnDestroy()
    {
        if (renderManager != null) renderManager.RemoveFromRenderer(this);
    }

    public virtual void SetHidden()
    {
        if (image != null)
        {
            Color fadeColor = image.color;
            fadeColor.a = 0f;
            image.DOColor(fadeColor, 0.5f);
        }
    }

    public virtual void SetOpacity()
    {

    }

    public virtual void SetOpaque()
    {
        if (image != null)
        {
            Color fadeColor = image.color;
            fadeColor.a = 1f;
            image.DOColor(fadeColor, 0.5f);
        }

    }

    public virtual GameObject _GameObject()
    {
        return gameObject;
    }

    public void SetColor(Color color)
    {
        
    }
}
