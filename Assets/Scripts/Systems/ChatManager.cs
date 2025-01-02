using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ChatManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject chatContent;
    [SerializeField] GameObject playerMessage;
    [SerializeField] GameObject enemyMessage;
    [SerializeField] GameObject systemMessage;
    [SerializeField] Button endButton;
    [SerializeField] GameObject endPanel;

    public static UnityEvent OnMessageSent = new UnityEvent();

    public static ChatManager instance;
    private void OnEnable()
    {
        BattleSystem.OnGameEnd.AddListener(() => SystemMessage("Game Ended. Continue with a new one!"));
        BattleSystem.OnGameEnd.AddListener(() => endPanel.SetActive(true));
        endButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        EnemyBattleHandler.OnEnemiesDead.AddListener(() => SystemMessage("You won! It's time for you dance!"));
        PlayerHealth.OnDeath.AddListener(() => SystemMessage("Looks like you've been defeated... but hey, at least you tried XD!"));
    }
    private void OnDisable()
    {
        OnMessageSent.RemoveAllListeners();
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
    }

    public void ManageMessage(string sender, string message)
    {
        GameObject textMessage = (sender == "You") ? playerMessage : enemyMessage;
        GameObject newText = Instantiate(textMessage, chatContent.transform);
        newText.GetComponent<TextMeshProUGUI>().text = $"{sender}: {message}";
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        chatContent.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;
        OnMessageSent?.Invoke();
    }
    public void SystemMessage(string message)
    {
        GameObject messageText = Instantiate(systemMessage, chatContent.transform);
        TextMeshProUGUI newText = messageText.GetComponent<TextMeshProUGUI>();
        newText.text = $"System: {message}";
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        OnMessageSent?.Invoke();
        chatContent.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;
    }
}
