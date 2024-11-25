using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemMessage : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI message;
    private void Start()
    {
        message = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        TickManager.instance.RegisterTimedTick(0.1f, () =>
        EffectManager.instance.TextRainbowEffect(message));
    }
    private void OnDisable()
    {
        TickManager.instance.UnregisterTimedTick(() =>
        EffectManager.instance.TextRainbowEffect(message));
    }
}
