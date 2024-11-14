using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerUIManager),typeof(PlayerPhraseManager))]
public class PlayerHealth : MonoBehaviour, IDamagable
{
    [Header("References")]
    [HideInInspector] PlayerPhraseManager phraseSystem;
    [HideInInspector] PlayerUIManager UISystem;
    [Header("Stats")]
    [SerializeField] public int currentHealth;

    public void Initialize()
    {
        phraseSystem = GetComponent<PlayerPhraseManager>();
        UISystem = GetComponent<PlayerUIManager>();
        currentHealth = CharacterManager.instance.selectedCharacter.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(UISystem.HitEffect());
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
        UserData data = SavingSystem.LoadPlayerData("PlayerSettings");
    }
}
