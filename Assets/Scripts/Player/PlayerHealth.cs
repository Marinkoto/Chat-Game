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

    public static event Action OnHit;

    public bool Hittable { get; set; }
    
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
        if (currentHealth <= 0)
            Die();
    }

    public void ReturnHealth(int healthToReturn)
    {
        currentHealth = Mathf.Min(currentHealth + healthToReturn, phraseSystem.selectedCharacter.maxHealth);
        EffectManager.instance.ChatEffect();
    }

    public void Die()
    {
        ChatManager.instance.SystemMessage("Looks like you've been defeated... but hey, at least you tried XD!");
        BattleSystem.instance.state = BattleState.End;
    }
}
