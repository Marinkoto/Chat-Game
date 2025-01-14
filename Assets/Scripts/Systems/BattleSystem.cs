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

public class BattleSystem : Singleton<BattleSystem>
{
    [Header("References")]
    [SerializeField] public BattleState state;
    [SerializeField] public PlayerBattleHandler playerHandler;
    [SerializeField] public EnemyBattleHandler enemyHandler;

    public static UnityEvent OnGameEnd = new UnityEvent();
    public CharacterData SelectedCharacter { get; private set; }
    private void OnDisable()
    {
        OnGameEnd.RemoveAllListeners();
    }

    public override void Awake()
    {
        isPersistent = false;
        SelectedCharacter = CharacterDataManager.Instance.selectedCharacter;    
    }
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        state = BattleState.PlayerTurn;
        StartBattleLoop();
        enemyHandler.EnemyUpdate(UserManager.Instance.data);
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
        ExperienceManager.Instance.AddExperience(UserManager.Instance.data.level * 150);
        CurrencyManager.AddCurrency(UserManager.Instance.data.level * 450, UserManager.Instance.data);
        OnGameEnd?.Invoke();
    }
}
    