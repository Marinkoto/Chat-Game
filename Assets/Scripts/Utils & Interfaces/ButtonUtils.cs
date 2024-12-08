using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUtils : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Components")]
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image targetButton;

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
        if (targetButton.sprite == buttonSprites[0])
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("Button Switch", AudioManager.instance.buttonSource, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("Button Click", AudioManager.instance.buttonSource, true);
    }
}
