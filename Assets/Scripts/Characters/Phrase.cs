using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PhraseType { DEFENCE, ATTACK, BUFF }
[CreateAssetMenu(fileName = "Phrase", menuName = "Phrases/Phrase", order = 1)]
public class Phrase : ScriptableObject
{
    [Header("Details")]
    [SerializeField, TextArea] public string phrase;
    [SerializeField] public PhraseType type;
    [SerializeField] public int phraseEffect;
    [SerializeField] public Sprite icon;
    [SerializeField, TextArea] public string description;

    public int GetPhraseDamage()
    {
        return Mathf.RoundToInt(phraseEffect * (UserManager.Instance.data.combatPower / 250f) * 1.75f);
    }
}
