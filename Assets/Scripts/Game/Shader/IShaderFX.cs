using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShaderFX
{

    public GameObject _GameObject();

    public void SetOpacity();

    public void SetHidden();

    public void SetOpaque();

    public void SetColor(Color color);
}
