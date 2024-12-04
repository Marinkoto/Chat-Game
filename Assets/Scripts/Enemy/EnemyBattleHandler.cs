using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBattleHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private List<GameObject> enemies;
    public int EnemyIndex { get; set; } = 0;

    public async Task HandleEnemyTurn()
    {
        await Task.Delay(2000);

        BattleSystem.instance.state = BattleState.PlayerTurn;
        if (enemy.healthSystem.IsDead())
        {
            ManageEnemies();
        }
        else
        {
            enemy.phraseSystem.ManagePhrases();
        }
        enemy.UISystem.ManageShield(enemy.healthSystem.Hittable);
    }
    private void ManageEnemies()
    {
        enemies.RemoveAt(EnemyIndex);
        if (enemies.Count > 0)
        {
            SpawnEnemy();
        }
        else
        {
            ChatManager.instance.SystemMessage("You’ve crushed the competition! " +
                "It's time for your dance!");
            BattleSystem.instance.state = BattleState.End;
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
}
