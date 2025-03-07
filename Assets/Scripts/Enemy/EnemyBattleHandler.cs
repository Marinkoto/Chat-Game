using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBattleHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] public List<GameObject> enemies;

    public static UnityEvent OnEnemiesDead = new UnityEvent();

    private CancellationTokenSource turnCancellationSource;
    public int EnemyIndex { get; set; } = 0;

    public async Task HandleEnemyTurn()
    {
        CancelTimer();
        turnCancellationSource = new CancellationTokenSource();
        await PauseMenu.WaitWhilePaused();
        await Task.Delay(2000, turnCancellationSource.Token);
        BattleSystem.Instance.state = BattleState.PlayerTurn;
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
        enemies.RemoveAt(EnemyIndex);
        if (enemies.Count > 0)
        {
            BattleSystem.Instance.state = BattleState.EnemyTurn;
            SpawnEnemy();
        }
        else
        {
            OnEnemiesDead?.Invoke();
            UserManager.Instance.data.wins++;
            BattleSystem.Instance.state = BattleState.End;
        }
    }
    public void SpawnEnemy()
    {
        EnemyIndex = Random.Range(0, enemies.Count);
        GameObject newEnemy = Instantiate(enemies[EnemyIndex], transform);
        enemy = newEnemy.GetComponent<Enemy>();
        PlayerPhraseManager.enemy = enemy;
    }
    public void EnemyUpdate(UserData data)
    {
        ListUtils.Shuffle(enemies);
        enemies.RemoveRange(0, enemies.Count - data.level);
    }
    private void CancelTimer()
    {
        turnCancellationSource?.Cancel();
    }
}
