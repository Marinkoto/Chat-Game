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
    [SerializeField] public int combatPower;
    [Header("Info")]
    [SerializeField, TextArea] public string @name;
    [Header("Visuals")]
    [SerializeField] public string animatorPath;
    [SerializeField] public Animator emotionAnimator;
    [SerializeField] public string iconPath;
    [HideInInspector] public Sprite icon;
}
