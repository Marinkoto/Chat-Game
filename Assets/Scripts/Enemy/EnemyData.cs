using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy",menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Data")]
    [SerializeField,TextArea] public string enemyName;
    [SerializeField] public Sprite icon;
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    
}
