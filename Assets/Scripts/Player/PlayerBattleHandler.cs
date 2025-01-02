using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerBattleHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [Header("Components")]
    [SerializeField] Slider timer;

    public static UnityEvent OnTimerTick = new UnityEvent();
    public static UnityEvent OnTimerEnd = new UnityEvent();
    private const float PLAYER_TIME_LIMIT = 20;
    public bool PlayerMadeChoice { get; set; }
    private CancellationTokenSource timerCancellationSource;

    private void OnDisable()
    {
        CancelTimer();
        OnTimerTick.RemoveAllListeners();
        OnTimerEnd.RemoveAllListeners();
    }

    public async Task HandlePlayerTurn()
    {
        player.UISystem.ManagePanel(true);
        PlayerMadeChoice = false;

        CancelTimer();
        timerCancellationSource = new CancellationTokenSource();

        var playerTurnTask = WaitUntilPlayerSelectsPhrase();
        var timerTask = ManagePlayerTimer(PLAYER_TIME_LIMIT, timerCancellationSource.Token);
        var completedTask = await Task.WhenAny(playerTurnTask, timerTask);

        if (completedTask == playerTurnTask)
        {
            PlayerMadeChoice = true;
        }

        timerCancellationSource.Cancel();
        player.UISystem.ManagePanel(false);
        player.UISystem.ManageShield(player.healthSystem.Hittable);

        BattleSystem.instance.state = BattleState.EnemyTurn;
    }
    private async Task ManagePlayerTimer(float timeLimit, CancellationToken cancellationToken)
    {
        float timeRemaining = timeLimit;
        timer.maxValue = timeLimit;
        timer.value = timeLimit;

        while (timeRemaining > 0 && !PlayerMadeChoice)
        {
            await Task.Delay(1000, cancellationToken);

            await PauseMenu.WaitWhilePaused();

            timeRemaining--;
            SliderUtils.ChangeColorByValue(timer, new Color(1, 0.075f, 0.075f), 5f);
            timer.value = Mathf.Max(0, timeRemaining);
            OnTimerTick?.Invoke();
        }

        if (timeRemaining <= 0 && !PlayerMadeChoice)
        {
            PlayerPhraseManager.OnPhraseSelected.RemoveAllListeners();
            OnTimerEnd?.Invoke();
            ChatManager.instance.SystemMessage("Looks like somebody is AFK...");
        }
    }

    private Task WaitUntilPlayerSelectsPhrase()
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();

        void onPhraseSelected()
        {
            taskCompletionSource.SetResult(true);
            PlayerPhraseManager.OnPhraseSelected.RemoveListener(onPhraseSelected);
        }

        PlayerPhraseManager.OnPhraseSelected.AddListener(onPhraseSelected);

        return taskCompletionSource.Task;
    }
    private void CancelTimer()
    {
        timerCancellationSource?.Cancel();
    }
}
