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
    [SerializeField] Animator animator;
    [SerializeField] public Enemy enemy;
    [SerializeField] Slider healthBar;
    [SerializeField] Image iconImage;
    [Header("References")]
    [SerializeField] CharacterData selectedCharacter;

    [HideInInspector] public bool phraseSelected = false;

    private void Start()
    {
        for (int i = 0; i < phraseButtons.Length; i++)
        {
            int index = i;
            phraseButtons[i].onClick.AddListener(() => SelectPhrase(index));
        }
        selectedCharacter = CharacterManager.instance.selectedCharacter;
        GetNewPhrases();
        SetHUD();
    }
    public void SelectPhrase(int index)
    {
        Phrase newPhrase = selectedCharacter.phrases[index];
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
        selectedCharacter.phrases.Shuffle();
        int phraseCount = Mathf.Min(phraseButtons.Length, selectedCharacter.phrases.Count);
        for (int i = 0; i < phraseCount; i++)
        {
            phraseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = selectedCharacter.phrases[i].phrase;
            phraseIcons[i].sprite = selectedCharacter.phrases[i].icon;
        }
    }

    public void ManagePanel(bool state)
    {
        animator.SetBool("IsOpen", state);
    }
    public void TakeDamage(int damage)
    {
        selectedCharacter.health -= damage;
        if (selectedCharacter.health <= 0)
            Die();
    }

    public void ReturnHealth(int healthToReturn)
    {
        if (selectedCharacter.health + healthToReturn > selectedCharacter.maxHealth)
        {
            selectedCharacter.health = selectedCharacter.maxHealth;
        }
        else
        {
            selectedCharacter.health += healthToReturn;
        }
    }
    public void SetHUD()
    {
        healthBar.value = selectedCharacter.health;
        healthBar.maxValue = selectedCharacter.maxHealth;
        iconImage.sprite = selectedCharacter.icon;
    }
    public void Die()
    {
        //TODO
    }
}
