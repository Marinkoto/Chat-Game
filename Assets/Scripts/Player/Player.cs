using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public PlayerHealth healthSystem;
    [SerializeField] public PlayerPhraseManager phraseSystem;
    [SerializeField] public PlayerUIManager UISystem;

    private void Start()
    {
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        SetupComponents();
        healthSystem.Initialize();
        phraseSystem.Initialize();
        UISystem.Initialize();
    }

    private void Update()
    {
        UISystem.HealthBarAnimation();
    }

    private void SetupComponents()
    {
        healthSystem = GetComponent<PlayerHealth>();
        phraseSystem = GetComponent<PlayerPhraseManager>();
        UISystem = GetComponent<PlayerUIManager>();
    }
}
