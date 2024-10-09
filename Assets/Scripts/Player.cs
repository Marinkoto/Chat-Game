using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamagable
{
    [Header("Components")]
    [SerializeField] Button[] phraseButtons;
    [SerializeField] Image[] phraseIcons;
    [SerializeField] List<Phrase> phraseList;
    [SerializeField] Animator animator;
    [SerializeField] public Enemy enemy;
    [SerializeField] Slider healthBar;
    [HideInInspector] public bool phraseSelected = false;
    [Header("Parameters")]
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    private void Start()
    {
        for (int i = 0; i < phraseButtons.Length; i++)
        {
            int index = i;
            phraseButtons[i].onClick.AddListener(() => SelectPhrase(index));
        }
        GetNewPhrases();
        SetHUD();
    }
    public void SelectPhrase(int index)
    {
        Phrase newPhrase = phraseList[index];
        ManagePanel(false);
        if (newPhrase.type == PhraseType.ATTACK)
        {
            enemy.TakeDamage(newPhrase.phraseEffect);
        }
        else
        if (newPhrase.type == PhraseType.DEFENCE)
        {
            this.ReturnHealth(newPhrase.phraseEffect);
        }
        phraseSelected = true;
        phraseSelected = false;
        ChatManager.instance.ManageMessage("You", $"{newPhrase.phrase}");
    }
    public void GetNewPhrases()
    {
        phraseList.Shuffle();
        int phraseCount = Mathf.Min(phraseButtons.Length, phraseList.Count);
        for (int i = 0; i < phraseCount; i++)
        {
            phraseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = phraseList[i].phrase;
            phraseIcons[i].sprite = phraseList[i].icon;
        }
    }

    public void ManagePanel(bool state)
    {
        animator.SetBool("IsOpen", state);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public void ReturnHealth(int healthToReturn)
    {
        if (health + healthToReturn > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healthToReturn;
        }
    }
    public void SetHUD()
    {
        healthBar.value = health;
        healthBar.maxValue = maxHealth;
    }
    public void Die()
    {
        //TODO
    }
}
