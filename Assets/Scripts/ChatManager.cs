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
        GameObject newText = Instantiate(systemMessage, chatContent.transform);
        TextMeshProUGUI messageText = newText.GetComponent<TextMeshProUGUI>();
        messageText.text = $"System: {message}";
        StartCoroutine(RainbowEffect(messageText, 0.1f));
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        chatContent.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;
    }
    private IEnumerator RainbowEffect(TextMeshProUGUI text, float speed)
    {

        Color[] rainbowColors = new Color[]
        {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            Color.magenta,
            Color.red
        };

        int colorCount = rainbowColors.Length;

        while (true)
        {
            for (int i = 0; i < colorCount; i++)
            {

                for (int j = 0; j < text.text.Length; j++)
                {
                    text.color = rainbowColors[(i + j) % colorCount];
                    yield return new WaitForSeconds(speed);
                }
            }
        }
    }
}
