using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Components")]
    [SerializeField] List<Phrase> phraseList;
    [SerializeField] Player player;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image icon;
    [Header("Parameters")]
    [SerializeField] EnemyData enemyData;
    [SerializeField] int currentHealth;
    private void Start()
    {
        currentHealth = enemyData.health;
        player = FindAnyObjectByType<Player>();
        SetHUD();
    }
    public void ReturnHealth(int healthToReturn)
    {
        if (currentHealth + healthToReturn > enemyData.maxHealth)
        {
            currentHealth = enemyData.maxHealth;
        }
        else
        {
            currentHealth += healthToReturn;
        }
    }
    public void ManagePhrase()
    {
        UsePhrase(phraseList[0]);
        GetNewPhrases();
    }
    public void UsePhrase(Phrase phraseToUse)
    {
        if(phraseToUse.type == PhraseType.ATTACK)
        {
            player.TakeDamage(phraseToUse.phraseEffect);
        }
        else 
        if (phraseToUse.type == PhraseType.DEFENCE)
        {
            this.ReturnHealth(phraseToUse.phraseEffect);
        }
        ChatManager.instance.ManageMessage("Enemy", $"{phraseToUse.phrase}");
    }
    public void SetHUD()
    {
        healthBar.value = currentHealth;
        healthBar.maxValue = enemyData.maxHealth;
        nameText.text = enemyData.enemyName;
        icon.sprite = enemyData.icon;
    }
    private void GetNewPhrases()
    {
        phraseList.Shuffle();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
}
