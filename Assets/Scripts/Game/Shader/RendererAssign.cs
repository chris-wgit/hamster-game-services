using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererAssign : MonoBehaviour, IShaderFX
{
    private CharacterRenderer render;
    void Start()
    {
        render = GetComponentInParent<CharacterRenderer>();
        if (render != null) render.AddObjectToRenderer(this);
    }

    private void OnDestroy()
    {
        if (render != null) render.RemoveFromRenderer(this);
    }

    GameObject IShaderFX._GameObject()
    {
        return gameObject;
    }

    public void SetOpacity()
    {
        
    }

    public void SetOpaque()
    {

    }
    public void SetHidden()
    {
        
    }

    public void SetColor(Color color)
    {
      
    }
}
