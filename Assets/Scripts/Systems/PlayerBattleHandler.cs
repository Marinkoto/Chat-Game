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

    private readonly float playerTimeLimit = 10;
    private bool playerMadeChoice;
    public async Task HandlePlayerTurn()
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

        BattleSystem.instance.state = BattleState.EnemyTurn;
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
}
