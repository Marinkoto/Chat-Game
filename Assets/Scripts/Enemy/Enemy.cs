using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPhraseManager), typeof(EnemyHealth), typeof(EnemyUIManager))]
public class Enemy : MonoBehaviour, ITickable
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
    private void OnEnable()
    {
        TickManager.Instance.Register(this);
    }
    private void OnDisable()
    {
        TickManager.Instance.Unregister(this);
    }
    private void SetupComponents()
    {
        healthSystem = GetComponent<EnemyHealth>();
        UISystem = GetComponent<EnemyUIManager>();
        phraseSystem = GetComponent<EnemyPhraseManager>();
    }

    public void Tick(float deltaTime)
    {
        if (UISystem == null)
            return;

         UISystem.HealthBarAnimation();
    }
}
