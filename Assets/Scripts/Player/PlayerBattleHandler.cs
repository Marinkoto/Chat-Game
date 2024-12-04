using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [Header("Components")]
    [SerializeField] Slider timer;

    private const float PLAYERTIMELIMIT = 10;
    public bool PlayerMadeChoice { get; set; }
   
    public async Task HandlePlayerTurn()
    {
        player.UISystem.ManagePanel(true);
        PlayerMadeChoice = false;

        var playerTurnTask = WaitUntilPlayerSelectsPhrase();
        var timerTask = StartPlayerTimer(PLAYERTIMELIMIT);
        var completedTask = await Task.WhenAny(playerTurnTask, timerTask);

        if (completedTask == playerTurnTask)
        {
            PlayerMadeChoice = true;
        }

        player.UISystem.ManagePanel(false);
        player.UISystem.ManageShield(player.healthSystem.Hittable);
        BattleSystem.instance.state = BattleState.EnemyTurn;
    }
    private async Task StartPlayerTimer(float timeLimit)
    {
        float timeRemaining = timeLimit;
        timer.maxValue = timeLimit;
        timer.value = timeLimit;

        float lastUpdateTime = Time.unscaledTime;

        while (timeRemaining > 0 && !PlayerMadeChoice)
        {
            await Task.Delay(100);

            if (PauseMenu.isPaused)
            {
                lastUpdateTime = Time.unscaledTime;
                continue;
            }

            float elapsed = Time.unscaledTime - lastUpdateTime;
            timeRemaining -= elapsed;
            timer.value = Mathf.Max(0, timeRemaining);
            lastUpdateTime = Time.unscaledTime;
        }

        if (timeRemaining <= 0 && !PlayerMadeChoice)
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
}
