using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [Header("Parameters")]
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;
    [SerializeField] public float scaleMultiplier;
    [Header("Components")]
    [HideInInspector] EnemyUIManager EnemyUISystem;

    public void Initialize()
    {
        EnemyUISystem = GetComponent<EnemyUIManager>();
        currentHealth = EnemyUISystem.enemyData.health;
        maxHealth = EnemyUISystem.enemyData.maxHealth;
        ScaleStats(CharacterManager.instance.selectedCharacter);
    }

    public void ReturnHealth(int healthToReturn)
    {
        currentHealth = Mathf.Min(currentHealth + healthToReturn, maxHealth);
        EnemyUISystem.SetHUD();
        EffectManager.instance.ChatEffect();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EffectManager.instance.ChatEffect();
        EnemyUISystem.SetHUD();
        if (IsDead())
            Die();
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    /// <summary>
    /// Scales Enemy stats based on character parameters and a scale multiplier
    /// </summary>
    /// <param name="character"></param>
    public void ScaleStats(CharacterData character)
    {
        currentHealth = Mathf.RoundToInt(character.maxHealth * scaleMultiplier + maxHealth  );
        maxHealth = currentHealth;
    }
}
