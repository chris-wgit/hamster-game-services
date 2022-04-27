using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class RendererSkinnedModel : RendererShaderBase, IShaderFX
{

    private SkinnedMeshRenderer meshRenderer;

    protected override void Awake()
    {
        base.Awake();
        if (renderManager != null) renderManager.AddObjectToRenderer(this);
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        materials = meshRenderer.materials;
        
    }
    private void OnDestroy()
    {
        if (renderManager != null) renderManager.RemoveFromRenderer(this);
    }

    public override void SetHidden()
    {
        base.SetHidden();

        meshRenderer.shadowCastingMode = ShadowCastingMode.Off;

    }
    public override void SetOpaque()
    {
        base.SetOpaque();

        meshRenderer.shadowCastingMode = ShadowCastingMode.On;


    }

}
