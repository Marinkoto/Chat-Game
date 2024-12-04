using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum BattleState { End, PlayerTurn, EnemyTurn }

public class BattleSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public BattleState state;
    [SerializeField] public PlayerBattleHandler playerHandler;
    [SerializeField] private EnemyBattleHandler enemyHandler;
    [SerializeField] private UserData data;

    [Header("Components")]
    [SerializeField] GameObject[] effects;

    public static BattleSystem instance;
    public CharacterData SelectedCharacter { get; private set; }

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
        SelectedCharacter = CharacterManager.instance.selectedCharacter;
    }
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        data = LoadingSystem.LoadUserData(UserData.SAVEKEY);
        state = BattleState.PlayerTurn;
        StartBattleLoop();
        enemyHandler.EnemyUpdate(data);
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
        ExperienceManager.instance.AddExperience(data.level * 150);
    }
}
