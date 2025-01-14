using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerUIManager), typeof(PlayerHealth))]
public class PlayerPhraseManager : MonoBehaviour
{
    [Header("Components")]
    public static Enemy enemy;
    [Header("References")]
    [HideInInspector] public CharacterData selectedCharacter;
    [HideInInspector] public PlayerHealth healthSystem;
    [HideInInspector] PlayerUIManager UISystem; 

    [HideInInspector] public static UnityEvent OnPhraseSelected = new();

    public void Initialize()
    {
        selectedCharacter = CharacterDataManager.Instance.selectedCharacter;
        healthSystem = GetComponent<PlayerHealth>();
        UISystem = GetComponent<PlayerUIManager>();
        for (int i = 0; i < UISystem.phraseButtons.Length; i++)
        {
            int index = i;
            UISystem.phraseButtons[i].onClick.AddListener(() => SelectPhrase(index));
        }

        UISystem.SetupPhrasesUI();
        UISystem.SetPlayerIcon();
    }

    public void SelectPhrase(int index)
    {
        Phrase newPhrase = selectedCharacter.phrases[index];
        UISystem.ManagePanel(false);
        healthSystem.Hittable = true;
        switch (newPhrase.type)
        {
            case PhraseType.DEFENCE:
                healthSystem.ReturnHealth(newPhrase.phraseEffect);
                break;
            case PhraseType.ATTACK:
                enemy.healthSystem.TakeDamage(newPhrase.GetPhraseDamage());
                break;
            case PhraseType.BUFF:
                healthSystem.Hittable = false;
                healthSystem.ReturnHealth(5);
                break;
            default:
                break;
        }
        OnPhraseSelected?.Invoke();
        ChatManager.Instance.ManageMessage("You", $"{newPhrase.phrase}");
    }
}
