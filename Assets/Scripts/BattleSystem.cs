using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { End, PlayerTurn, EnemyTurn }
public class BattleSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Player player;
    [SerializeField] BattleState state;
    [SerializeField] List<GameObject> enemies;

    private int currentEnemyIndex = 0;
    private void Start()
    {
        state = BattleState.PlayerTurn;
        SpawnEnemy();
    }
    private IEnumerator PlayerTurn()
    {
        if (state != BattleState.PlayerTurn)
            yield return null;

        yield return new WaitForSeconds(2f);
        player.ManagePanel(true);
        yield return new WaitUntil(() => player.phraseSelected);
        enemy.SetHUD();
        state = BattleState.EnemyTurn;
    }
    private IEnumerator EnemyTurn()
    {
        if (state != BattleState.EnemyTurn)
            yield return null;

        enemy.SetHUD();
        yield return new WaitForSeconds(2f);
        enemy.ManagePhrase();
        player.SetHUD();
        state = BattleState.PlayerTurn;
        if (enemy.IsDead())
        {
            ManageEnemies();
        }
        else
        {
            StartCoroutine(PlayerTurn());
        }

    }
    public void OnPhraseSelected()
    {
        if (state != BattleState.PlayerTurn)
            return;
        StartCoroutine(EnemyTurn());
    }
    private void ManageEnemies()
    {
        Destroy(enemies[currentEnemyIndex]);
        enemies.RemoveAt(currentEnemyIndex);
        if (enemies.Count > 0)
        {
            SpawnEnemy();
            StartCoroutine(PlayerTurn());
        }
        else
        {
            Debug.Log("Player Wins!");
        }
    }
    private void SpawnEnemy()
    {
        if (currentEnemyIndex >= enemies.Count)
        {
            state = BattleState.End;
            Debug.Log("Player Wins! No more enemies.");
            return;
        }

        GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform);
        enemy = newEnemy.GetComponent<Enemy>();
        enemy.SetHUD();
        player.enemy = enemy;
    }
}
