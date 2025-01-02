using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum BattleState { End, PlayerTurn, EnemyTurn }

public class BattleSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public BattleState state;
    [SerializeField] public PlayerBattleHandler playerHandler;
    [SerializeField] private EnemyBattleHandler enemyHandler;

    public static BattleSystem instance;
    public static UnityEvent OnGameEnd = new UnityEvent();
    public CharacterData SelectedCharacter { get; private set; }
    private void OnDisable()
    {
        OnGameEnd.RemoveAllListeners();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        SelectedCharacter = CharacterDataManager.instance.selectedCharacter;
    }
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        state = BattleState.PlayerTurn;
        StartBattleLoop();
        enemyHandler.EnemyUpdate(UserManager.instance.data);
        enemyHandler.SpawnEnemy();
    }

    private async void StartBattleLoop()
    {
        while (state != BattleState.End)
        {
            await Task.Delay(300);
            if (state == BattleState.PlayerTurn && !PauseMenu.isPaused)
            {
                await playerHandler.HandlePlayerTurn();
            }
            else if (state == BattleState.EnemyTurn && !PauseMenu.isPaused)
            {
                await enemyHandler.HandleEnemyTurn();
            }
        }
        EndGame();
    }
    public void EndGame()
    {
        ExperienceManager.instance.AddExperience(UserManager.instance.data.level * 150);
        CurrencyManager.AddCurrency(UserManager.instance.data.level * 250, UserManager.instance.data);
        OnGameEnd?.Invoke();
    }
}
    