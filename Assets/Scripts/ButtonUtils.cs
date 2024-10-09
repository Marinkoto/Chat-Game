using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUtils : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image targetButton;
    private void Start()
    {
        targetButton.GetComponent<Button>().onClick.AddListener(() => SwapSprites());
    }
    public void SwapSprites()
    {
        if (targetButton.sprite == buttonSprites[0])
        {
            targetButton.sprite = buttonSprites[1];
            return;
        }
        targetButton.sprite = buttonSprites[0];
    }
    public void OpenPanel(GameObject panel)
    {
        if(targetButton.sprite == buttonSprites[0])
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
