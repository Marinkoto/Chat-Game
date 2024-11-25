using UnityEngine;

[RequireComponent(typeof(PlayerPhraseManager), typeof(PlayerHealth), typeof(PlayerUIManager))]
public class Player : MonoBehaviour,ITickable
{
    [Header("References")]
    [SerializeField] public PlayerHealth healthSystem;
    [SerializeField] public PlayerPhraseManager phraseSystem;
    [SerializeField] public PlayerUIManager UISystem;

    private void Start()
    {
        SetupPlayer();
    }

    private void OnEnable()
    {
        TickManager.instance.Register(this);
    }
    private void OnDisable()
    {
        TickManager.instance.Unregister(this);
    }

    private void SetupPlayer()
    {
        SetupComponents();
        healthSystem.Initialize();
        phraseSystem.Initialize();
        UISystem.Initialize();
    }

    private void SetupComponents()
    {
        healthSystem = GetComponent<PlayerHealth>();
        phraseSystem = GetComponent<PlayerPhraseManager>();
        UISystem = GetComponent<PlayerUIManager>();
    }

    public void Tick(float deltaTime)
    {
        UISystem.HealthBarAnimation();
    }
}
