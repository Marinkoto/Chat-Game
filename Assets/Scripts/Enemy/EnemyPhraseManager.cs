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

    /// <summary>
    /// Setups all components for the script to work and sends a starting message for new enemy.
    /// </summary>
    public void Initialize()
    {
        healthSystem = GetComponent<EnemyHealth>();
        ChatManager.instance.SystemMessage("Brace yourself! New challenger incoming!");
        UISystem = GetComponent<EnemyUIManager>();
        player = FindAnyObjectByType<Player>();    
    }

    public void ManagePhrases()
    {
        UsePhrase(phraseList[Random.Range(0, phraseList.Count)]);
        GetNewPhrases();
    }
    private void UsePhrase(Phrase phraseToUse)
    {
        healthSystem.Hittable = true;
        switch (phraseToUse.type)
        {
            case PhraseType.DEFENCE:
                healthSystem.ReturnHealth(phraseToUse.phraseEffect);
                break;
            case PhraseType.ATTACK:
                player.healthSystem.TakeDamage(phraseToUse.phraseEffect);
                break;
            case PhraseType.BUFF:
                healthSystem.Hittable = false;
                break;
            default:
                break;
        }
        ChatManager.instance.ManageMessage("Enemy", $"{phraseToUse.phrase}");
        UISystem.ManageShield(healthSystem.Hittable);
    }
    private void GetNewPhrases()
    {
        phraseList.Shuffle();
    }
}
