using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPhraseManager : MonoBehaviour
{
    [Header("Components")]
    public Enemy enemy;
    [Header("References")]
    [HideInInspector] public CharacterData selectedCharacter;
    [SerializeField] public PlayerHealth healthSystem;
    [SerializeField] PlayerUIManager UISystem;

    [HideInInspector] public UnityEvent onPhraseSelected = new UnityEvent();

    public void Initialize()
    {
        selectedCharacter = CharacterManager.instance.selectedCharacter;
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

        switch (newPhrase.type)
        {
            case PhraseType.DEFENCE:
                healthSystem.ReturnHealth(newPhrase.phraseEffect);
                break;
            case PhraseType.ATTACK:
                enemy.healthSystem.TakeDamage((int)(Mathf.RoundToInt(newPhrase.phraseEffect * selectedCharacter.combatPower) * 0.01f));
                break;
            case PhraseType.BUFF:
                BattleSystem.instance.state = BattleState.PlayerTurn;
                break;
            default:
                break;
        }

        onPhraseSelected.Invoke();
        ChatManager.instance.ManageMessage("You", $"{newPhrase.phrase}");
    }
}
