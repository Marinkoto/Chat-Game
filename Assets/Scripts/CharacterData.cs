using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [Header("Stats")]
    [SerializeField] public List<Phrase> phrases;  
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    [Header("Info")]
    [SerializeField, TextArea] public string @name;
    [Header("Visuals")]
    [SerializeField] public Sprite icon;
}
