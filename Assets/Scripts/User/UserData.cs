using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    [Header("Level Data")]
    [SerializeField] public int level = 1;
    [SerializeField] public int maxLevel = 40;
    [SerializeField] public int currentExp = 0;
    [SerializeField] public int expToLevelUp = 150;
    [SerializeField] public int costToLevelUp = 500;
    [Header("Currency Data")]
    [SerializeField] public int currency = 5000;
    [Header("Game Data")]
    [SerializeField] public int combatPower = 250;
    [SerializeField] public int wins = 0;
    [SerializeField] public static readonly string SAVE_KEY = "PlayerSettings";
}
