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
    [SerializeField] public int costToUpgrade = 150;
    [SerializeField] public int level = 1;
    [SerializeField] public int maxLevel = 50;
    [Header("Info")]
    [SerializeField, TextArea] public string @name;
    [Header("Visuals")]
    [SerializeField] public string iconPath;
    [HideInInspector] public Sprite icon;
    [Header("Equipment")]
    [SerializeField] public Equipment weapon;

    public bool IsMaxLevel()
    {
        return level >= maxLevel;
    }
}
