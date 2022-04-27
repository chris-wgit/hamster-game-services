using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RendererSpriteBase : MonoBehaviour, IShaderFX
{
    protected CharacterRenderer renderManager;
    protected SpriteRenderer sprite;

    protected virtual void Awake()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
        renderManager = GetComponentInParent<CharacterRenderer>();
        if (renderManager != null) renderManager.AddObjectToRenderer(this);
    }
    private void OnDestroy()
    {
        if (renderManager != null) renderManager.RemoveFromRenderer(this);
    }

    public virtual void SetHidden()
    {
        if (sprite != null)
        {
            Color fadeColor = sprite.color;
            fadeColor.a = 0f;
            sprite.DOColor(fadeColor, 0.5f);
        }
    }

    public virtual void SetOpacity()
    {
        if (sprite != null)
        {
            Color fadeColor = sprite.color;
            fadeColor.a = 0.5f;
            sprite.DOColor(fadeColor, 0.5f);
        }
    }

    public virtual void SetOpaque()
    {
        if (sprite != null)
        {
            Color fadeColor = sprite.color;
            fadeColor.a = 1f;
            sprite.DOColor(fadeColor, 0.5f);
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
