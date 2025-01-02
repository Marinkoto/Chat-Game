using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI currencyText;
    private void Start()
    {
        TickManager.instance.RegisterTimedTick(0.75f,UpdateUI);
    }
    private void OnDisable()
    {
        TickManager.instance.UnregisterTimedTick(UpdateUI);
    }
    private void UpdateUI()
    {
        currencyText.text = UserManager.instance.data.currency.ToString();
    }
}
