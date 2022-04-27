using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RendererShaderBase : MonoBehaviour, IShaderFX
{
    protected CharacterRenderer renderManager;
    protected Material[] materials;

    private float outlineWidth = 6;

    protected virtual void Awake()
    {
        renderManager = GetComponentInParent<CharacterRenderer>();
        if (renderManager != null) renderManager.AddObjectToRenderer(this);
    }
    private void OnDestroy()
    {
        if (renderManager != null) renderManager.RemoveFromRenderer(this);
    }
    private void Start()
    {
        //outlineWidth = materials[0].GetFloat("_OutlineWidth");
    }
    public virtual void SetHidden()
    {
        if (materials != null)
        {
            foreach (Material m in materials)
            {
                {
                    Color fadeColor = m.color;
                    fadeColor.a = 0f;
                    m.DOColor(fadeColor, 0.5f);
                    m.DOFloat(0f, "_OutlineWidth",  0.5f);

                    m.renderQueue = 2500;
                }
            }
        }


    }

    public virtual void SetOpacity()
    {
        if (materials != null)
        {
            foreach (Material m in materials)
            {
                {
                    Color fadeColor = m.color;
                    fadeColor.a = 0.8f;
                    m.DOColor(fadeColor, 0.5f);

                    m.renderQueue = 2400;
                }
            }
        }
    }

    public virtual void SetOpaque()
    {
        if (materials != null)
        {
            foreach (Material m in materials)
            {
                {
                    Color opaqueColor = m.color;
                    opaqueColor.a = 1f;
                    m.DOColor(opaqueColor, 0.5f);
                    m.SetFloat("_OutlineWidth", outlineWidth);

                    m.renderQueue = 2400;
                }
            }
        }

    }

    public virtual GameObject _GameObject()
    {
        return gameObject;
    }

    public void SetColor(Color color)
    {
        if (materials != null)
        {
            foreach (Material m in materials)
            {
                m.DOPause();
                m.color = color;
                m.DOColor(Color.white, 2f);
            }
        }
    }
}
