using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject[] chatEffects;
    [SerializeField] public Volume chatVolume;
    public static EffectManager instance;

    //Used for rainbow effect to track ticks
    public int TickCount { get; set; } = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChatEffect()
    {
        Vector3 randPos = new(Random.Range(-5, 5), Random.Range(-3, 5));
        Instantiate(chatEffects[Random.Range(0, chatEffects.Length)], randPos, Quaternion.identity);
    }
    public IEnumerator PlayerHitEffect()
    {
        ChatEffect();
        chatVolume.enabled = true;
        yield return new WaitForSeconds(0.17f);
        chatVolume.enabled = false;
    }
    public void TextRainbowEffect(TextMeshProUGUI text)
    {
        if (text == null)
            return;
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

        int colorIndex = TickCount % rainbowColors.Length;

        // Apply the current color to the text
        text.color = rainbowColors[colorIndex];

        // Increment the tick counter
        TickCount++;
    }
}