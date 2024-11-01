using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public EnemyUIManager UISystem;
    [SerializeField] public EnemyHealth healthSystem;
    [SerializeField] public EnemyPhraseManager phraseSystem;

    private void Start()
    {
        SetupComponents();
        phraseSystem.Initialize();
        healthSystem.Initialize();
        UISystem.Initialize();
    }
    private void SetupComponents()
    {
        healthSystem = GetComponent<EnemyHealth>();
        UISystem = GetComponent<EnemyUIManager>();
        phraseSystem = GetComponent<EnemyPhraseManager>();
    }
}
