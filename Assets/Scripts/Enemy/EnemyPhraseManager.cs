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

    /// <summary>
    /// Setups all components for the script to work and sends a starting message for new enemy.
    /// </summary>
    public void Initialize()
    {
        healthSystem = GetComponent<EnemyHealth>();
        ChatManager.instance.SystemMessage("Brace yourself! New challenger incoming!");
        player = FindAnyObjectByType<Player>();
    }

    public void ManagePhrases()
    {
        UsePhrase(ListUtils.GetRandomItem(phraseList));
        GetNewPhrases();
    }
    private void UsePhrase(Phrase phraseToUse)
    {
        healthSystem.Hittable = true;
        switch (phraseToUse.type)
        {
            case PhraseType.DEFENCE:
                healthSystem.ReturnHealth(Mathf.RoundToInt(phraseToUse.phraseEffect * healthSystem.scaleMultiplier));
                break;
            case PhraseType.ATTACK:
                player.healthSystem.TakeDamage(Mathf.RoundToInt(phraseToUse.phraseEffect * healthSystem.scaleMultiplier * 1.35f));
                break;
            case PhraseType.BUFF:
                healthSystem.Hittable = false;
                healthSystem.ReturnHealth(5);
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
