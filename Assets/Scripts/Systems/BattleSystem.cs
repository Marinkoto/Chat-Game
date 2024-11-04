using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum BattleState { End, PlayerTurn, EnemyTurn, Paused }

public class BattleSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Player player;
    [SerializeField] public BattleState state;
    [SerializeField] public List<GameObject> enemies;
    [SerializeField] public CharacterData selectedCharacter;

    [Header("Player Timer")]
    [SerializeField] private int enemyIndex = 0;
    [SerializeField] private readonly float playerTimeLimit = 10f;
    [SerializeField] private bool playerMadeChoice;
    [SerializeField] Slider timer;

    [Header("Components")]
    [SerializeField] GameObject[] effects;

    public static BattleSystem instance;

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
        selectedCharacter = CharacterManager.instance.selectedCharacter;
    }
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        state = BattleState.PlayerTurn;
        player = FindAnyObjectByType<Player>();
        StartBattleLoop();
        EnemyUpdate();
        SpawnEnemy();
        
    }

    private async void StartBattleLoop()
    {
        await Task.Delay(150);
        while (state != BattleState.End)
        {
            if (state == BattleState.PlayerTurn && !PauseMenu.isPaused)
            {
                await HandlePlayerTurn();
            }
            else if (state == BattleState.EnemyTurn && !PauseMenu.isPaused)
            {
                await HandleEnemyTurn();
            }
        }
    }

    private async Task HandlePlayerTurn()
    {
        player.UISystem.ManagePanel(true);
        playerMadeChoice = false;

        var playerTurnTask = WaitUntilPlayerSelectsPhrase();
        var timerTask = StartPlayerTimer(playerTimeLimit);

        var completedTask = await Task.WhenAny(playerTurnTask, timerTask);

        if (completedTask == playerTurnTask)
        {
            playerMadeChoice = true;
        }

        player.UISystem.ManagePanel(false);

        state = BattleState.EnemyTurn;
    }

    private async Task StartPlayerTimer(float timeLimit)
    {
        float timeRemaining = timeLimit;
        timer.maxValue = timeLimit;
        timer.value = timeLimit;
        while (timeRemaining > 0 && !playerMadeChoice)
        {
            await Task.Delay(1000);
            timeRemaining--;
            timer.value = timeRemaining;
        }

        if (!playerMadeChoice)
        {
            player.phraseSystem.onPhraseSelected.RemoveAllListeners();
            ChatManager.instance.SystemMessage("Looks like somebody is AFK...");
        }
    }

    private Task WaitUntilPlayerSelectsPhrase()
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();

        void onPhraseSelected()
        {
            taskCompletionSource.SetResult(true);
            player.phraseSystem.onPhraseSelected.RemoveListener(onPhraseSelected);
        }

        player.phraseSystem.onPhraseSelected.AddListener(onPhraseSelected);

        return taskCompletionSource.Task;
    }

    private async Task HandleEnemyTurn()
    {
        await Task.Delay(2000);

        state = BattleState.PlayerTurn;
        if (enemy.healthSystem.IsDead())
        {
            ManageEnemies();
        }
        else
        {
            enemy.phraseSystem.ManagePhrases();
        }
    }

    private void ManageEnemies()
    {
        enemies.RemoveAt(enemyIndex);
        if (enemies.Count > 0)
        {
            SpawnEnemy();
        }
        else
        {
            ChatManager.instance.SystemMessage("You’ve crushed the competition! " +
                "It's time for your dance!");
            state = BattleState.End;
        }
    }

    private void SpawnEnemy()
    {
        enemyIndex = Random.Range(0, enemies.Count);
        GameObject newEnemy = Instantiate(enemies[enemyIndex], transform);
        enemy = newEnemy.GetComponent<Enemy>();
        player.phraseSystem.enemy = enemy.GetComponent<Enemy>();
    }
    private void EnemyUpdate()
    {
        ListUtils.Shuffle(enemies);
        enemies.RemoveRange(0, enemies.Count - player.data.level);
    }
    private void EndGame()
    {
        ExperienceManager.instance.AddExperience(player.data.level * 150);
    }
}
