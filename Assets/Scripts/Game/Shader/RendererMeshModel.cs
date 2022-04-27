using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshRenderer))]
public class RendererMeshModel : RendererShaderBase, IShaderFX
{

    private MeshRenderer meshRenderer;

    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        materials = meshRenderer.materials;
    }
    private void OnDestroy()
    {
        if (renderManager != null) renderManager.RemoveFromRenderer(this);
        meshRenderer.enabled = false;
    }

    public override void SetHidden()
    {
        base.SetHidden();
        meshRenderer.enabled = false;
        meshRenderer.shadowCastingMode = ShadowCastingMode.Off;

    }

    public override void SetOpaque()
    {
        base.SetOpaque();
        meshRenderer.enabled = false;
        meshRenderer.shadowCastingMode = ShadowCastingMode.On;
    }
}
