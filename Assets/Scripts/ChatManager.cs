using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject chatContent;
    [SerializeField] GameObject playerMessage;
    [SerializeField] GameObject enemyMessage;

    public static ChatManager instance;

    private void Awake()
    {
        if(instance == null)
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
        newText.GetComponent<TextMeshProUGUI>().text = $"{sender}: {message}" ;
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        chatContent.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;
    }
}
