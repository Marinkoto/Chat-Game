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

    [HideInInspector] public UnityEvent onPhraseSelected = new();
    public UserData Data { get; set; }

    public void Initialize()
    {
        selectedCharacter = CharacterDataManager.instance.selectedCharacter;
        healthSystem = GetComponent<PlayerHealth>();
        UISystem = GetComponent<PlayerUIManager>();
        Data = LoadingSystem.LoadUserData(UserData.SAVEKEY);
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
                enemy.healthSystem.TakeDamage((int)(Mathf.RoundToInt(newPhrase.phraseEffect * Data.combatPower) * 0.01f));
                break;
            case PhraseType.BUFF:
                healthSystem.Hittable = false;
                healthSystem.ReturnHealth(10);
                break;
            default:
                break;
        }
        onPhraseSelected.Invoke();
        ChatManager.instance.ManageMessage("You", $"{newPhrase.phrase}");
    }
}
