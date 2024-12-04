using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] public EnemyData enemyData;
    [SerializeField] EnemyHealth healthSystem;
    [SerializeField] Animator shield;

    private void OnEnable()
    {
        EnemyHealth.OnHit += OnEnemyHitHandler;
    }

    private void OnDisable()
    {
        EnemyHealth.OnHit -= OnEnemyHitHandler;
    }
    public void HealthBarAnimation()
    {
        if(healthBar != null)
        {
            SliderUtils.SmoothAnimation(healthBar, 2, healthSystem.currentHealth);
        }
    }

    public void Initialize()
    {
        healthSystem = GetComponent<EnemyHealth>();
        SetHUD();
        InitializeHealthBar();
    }
    /// <summary>
    /// Sets name and icon for enemy
    /// </summary>
    public void SetHUD()
    {
        nameText.text = enemyData.enemyName;
        icon.sprite = enemyData.icon;
        healthText.text = $"{healthSystem.currentHealth}/{healthSystem.maxHealth}";
    }
    private void InitializeHealthBar()
    {
        healthBar.maxValue = healthSystem.maxHealth;
        healthBar.value = healthSystem.currentHealth;
    }
    public void ManageShield(bool state)
    {
        if (shield == null)
            return;
        shield.SetBool("Active",!state);
    }
    private void OnEnemyHitHandler()
    {
        ManageShield(healthSystem.Hittable);
    }
}
