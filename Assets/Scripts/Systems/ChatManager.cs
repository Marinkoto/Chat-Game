using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ChatManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject chatContent;
    [SerializeField] GameObject playerMessage;
    [SerializeField] GameObject enemyMessage;
    [SerializeField] GameObject systemMessage;

    public static ChatManager instance;

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
    }
    public void SystemMessage(string message)
    {
        GameObject messageText = Instantiate(systemMessage, chatContent.transform);
        TextMeshProUGUI newText = messageText.GetComponent<TextMeshProUGUI>();
        newText.text = $"System: {message}";
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        chatContent.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;
    }
}
