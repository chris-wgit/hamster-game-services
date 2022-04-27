using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UISpriteBehaviour : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public Color OwnerColor;
    public Color AllyColor;
    public Color EnemyColor;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void SetAllySprite()
    {
        _renderer.color = AllyColor;
    }

    public void SetEnemySprite()
    {
        _renderer.color = EnemyColor;
    }
}
