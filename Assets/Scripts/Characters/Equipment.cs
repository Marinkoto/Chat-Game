using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    [Header("Parameters")]
    [SerializeField,TextArea] public string @name;
    [SerializeField, TextArea] public string description;
    [SerializeField] public int upgradeCost = 150;
    [SerializeField] public int level = 1;
    [SerializeField] public int maxLevel = 20;
    [SerializeField] public int combatPowerPerUpgrade = 2;
    [SerializeField] public Sprite icon;
    [SerializeField] public string iconPath;
}

