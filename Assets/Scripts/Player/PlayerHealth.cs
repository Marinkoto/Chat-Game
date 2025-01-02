using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerUIManager), typeof(PlayerPhraseManager))]
public class PlayerHealth : MonoBehaviour, IDamagable
{
    [Header("References")]
    [HideInInspector] PlayerPhraseManager phraseSystem;
    [Header("Stats")]
    [SerializeField] public int currentHealth;

    public static UnityEvent OnHit = new UnityEvent();
    public static UnityEvent OnHealthChange = new UnityEvent();
    public static UnityEvent OnDeath = new UnityEvent();
    public bool Hittable { get; set; }

    private void OnEnable()
    {
        EnemyHealth.OnDeath.AddListener(() => ReturnHealth(50));
    }
    private void OnDisable()
    {
        OnHit.RemoveAllListeners();
        OnHealthChange.RemoveAllListeners();
        OnDeath.RemoveAllListeners();
    }
    public void Initialize()
    {
        phraseSystem = GetComponent<PlayerPhraseManager>();
        currentHealth = CharacterDataManager.instance.selectedCharacter.maxHealth;
        Hittable = true;
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
        StartCoroutine(EffectManager.instance.PlayerHitEffect());
        currentHealth -= damage;
        if (IsDead())
            Die();
    }

    public void ReturnHealth(int healthToReturn)
    {
        currentHealth = Mathf.Min(currentHealth + healthToReturn, phraseSystem.selectedCharacter.maxHealth);
        EffectManager.instance.ChatEffect();
        OnHealthChange?.Invoke();
    }
    private bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void Die()
    {
        OnDeath?.Invoke();
        BattleSystem.instance.state = BattleState.End;
    }
}
