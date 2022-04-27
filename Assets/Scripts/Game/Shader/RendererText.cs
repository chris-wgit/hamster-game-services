using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class RendererText : MonoBehaviour, IShaderFX
{
    protected CharacterRenderer renderManager;
    protected TextMeshProUGUI text;

    protected virtual void Awake()
    {
        text = GetComponentInParent<TextMeshProUGUI>();
        renderManager = GetComponentInParent<CharacterRenderer>();
        if (renderManager != null) renderManager.AddObjectToRenderer(this);
    }
    private void OnDestroy()
    {
        if (renderManager != null) renderManager.RemoveFromRenderer(this);
    }

    public virtual void SetHidden()
    {
        if (text != null)
        {
            Color fadeColor = text.color;
            fadeColor.a = 0f;
            text.DOColor(fadeColor, 0.5f);
        }
    }

    public virtual void SetOpacity()
    {

    }

    public virtual void SetOpaque()
    {
        if (text != null)
        {
            Color fadeColor = text.color;
            fadeColor.a = 1f;
            text.DOColor(fadeColor, 0.5f);
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
