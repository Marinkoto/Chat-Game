using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemMessage : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI message;
    private void Awake()
    {
        message = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        if (message != null)
        {
            TickManager.Instance.RegisterTimedTick(0.1f, RainbowEffect);
        }
    }
    private void OnDisable()
    {
        TickManager.Instance.UnregisterTimedTick(RainbowEffect);
    }
    private void RainbowEffect()
    {
        EffectManager.Instance.TextRainbowEffect(message);
    }
}
