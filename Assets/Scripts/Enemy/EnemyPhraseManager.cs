using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPhraseManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] List<Phrase> phraseList;
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI nameText;
    [HideInInspector] public EnemyHealth healthSystem;
    [HideInInspector] public EnemyUIManager UISystem;
    [Header("Parameters")]
    [HideInInspector] BattleSystem battleSystem;
    
    public void Initialize()
    {
        healthSystem = GetComponent<EnemyHealth>();
        ChatManager.instance.SystemMessage("Brace yourself! New challenger incoming!");
        UISystem = GetComponent<EnemyUIManager>();
        battleSystem = GameObject.FindObjectOfType<BattleSystem>();
        player = FindAnyObjectByType<Player>();
    }

    public void ManagePhrases()
    {
        UsePhrase(phraseList[Random.Range(0, phraseList.Count)]);
        GetNewPhrases();
    }
    private void UsePhrase(Phrase phraseToUse)
    {
        switch (phraseToUse.type)
        {
            case PhraseType.DEFENCE:
                healthSystem.ReturnHealth(phraseToUse.phraseEffect);
                break;
            case PhraseType.ATTACK:
                player.healthSystem.TakeDamage(phraseToUse.phraseEffect);
                break;
            case PhraseType.BUFF:
                battleSystem.state = BattleState.EnemyTurn;
                break;
            default:
                break;
        }
        ChatManager.instance.ManageMessage("Enemy", $"{phraseToUse.phrase}");
    }
    private void GetNewPhrases()
    {
        phraseList.Shuffle();
    }
}
