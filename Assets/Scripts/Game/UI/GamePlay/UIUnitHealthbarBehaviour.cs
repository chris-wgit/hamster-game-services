using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class UIUnitHealthbarBehaviour : MonoBehaviour
{
    public Health health;

    public UISliderBehaviour healthSlider;
    public UISliderBehaviour healthLossSlider;

    [Header("HealthBar UI")]
    public Image healthBarImg;
    public Sprite healthBarSpriteFriendly;
    public Sprite healthBarSpriteEnemy;


    public UISliderSeparator separator;

    private void Awake()
    {
        if (health == null) health = GetComponentInParent<Health>();
    }
    private void OnEnable()
    {
        health.OnCurrentHealthChanged += SetCurrentHealth;
        health.OnMaxHealthChanged += SetUpHealthBar;
    }
    private void OnDisable()
    {
        health.OnCurrentHealthChanged -= SetCurrentHealth;
        health.OnMaxHealthChanged -= SetUpHealthBar;
    }

    public void SetUpHealthBar(int health)
    {
        SetMaxHealth(health);
        SetCurrentHealth(health);
    }

    #region Value Function

    public void SetMaxHealth(int value)
    {
        healthSlider.SetMaxValue(value);
        healthLossSlider.SetMaxValue(value);
    }

    public void SetCurrentHealth(int value)
    {
        healthSlider.SetCurrentValue(value);
        healthLossSlider.SetCurrentValueAnimation(value);
    }

    #endregion

    #region DesignFunction

    public void SetFriendlyHealthBar()
    {
        healthBarImg.sprite = healthBarSpriteFriendly;
    }

    public void SetEnemyHealthBar()
    {
        healthBarImg.sprite = healthBarSpriteEnemy;
    }

    #endregion

    void InCreaseHealth()
    {
        SetUpHealthBar(500);

    }
}

