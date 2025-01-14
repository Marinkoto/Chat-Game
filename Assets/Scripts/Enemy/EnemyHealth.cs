using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [Header("Parameters")]
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;
    [SerializeField] public float scaleMultiplier;
    [Header("Components")]
    [SerializeField] EnemyUIManager EnemyUISystem;

    public static UnityEvent OnHealthChange = new UnityEvent();
    public static UnityEvent OnHit = new UnityEvent();
    public static UnityEvent OnDeath = new UnityEvent();
    public bool Hittable { get; set; }

    /// <summary>
    /// Sets all the components and manages stats, systems.
    /// </summary>
    private void OnDisable()
    {
        OnHit.RemoveAllListeners();
        OnHealthChange.RemoveAllListeners();
        OnDeath.RemoveAllListeners();
    }
    public void Initialize()
    {
        EnemyUISystem = GetComponent<EnemyUIManager>();
        currentHealth = EnemyUISystem.enemyData.health;
        maxHealth = EnemyUISystem.enemyData.maxHealth;
        ScaleStats(CharacterDataManager.Instance.selectedCharacter);
        Hittable = true;
    }

    public void ReturnHealth(int healthToReturn)
    {
        currentHealth = Mathf.Min(currentHealth + healthToReturn, maxHealth);
        EnemyUISystem.SetHUD();
        EffectManager.Instance.ChatEffect();
        OnHealthChange?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        OnHit?.Invoke();
        if (!Hittable)
        {
            Hittable = true;
            OnHit?.Invoke();
            return;
        }

        currentHealth -= damage;
        EffectManager.Instance.ChatEffect();
        EnemyUISystem.SetHUD();
        if (IsDead())
            Die();
    }
    public void Die()
    {
        OnDeath?.Invoke();
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
        currentHealth = Mathf.RoundToInt(character.maxHealth * scaleMultiplier + maxHealth);
        maxHealth = currentHealth;
    }
}
