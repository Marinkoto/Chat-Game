using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI equipmentStats;
    [SerializeField] Button upgradeButton;
    [SerializeField] Image itemIcon;
    public static EquipmentManager instance;

    
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

    public void UpdateEquipment(Equipment equipment)
    {
        if (CurrencyManager.HasCurrency(UserManager.instance.Data,equipment.upgradeCost))
        {
            equipment.upgradeCost += 250;
            equipment.level++;
            UserManager.instance.Data.combatPower += equipment.combatPowerPerUpgrade;
        }
    }
    public void UpdateUI(Equipment equipment)
    {
        //equipmentStats.text = $"{equipment.level}/{equipment.maxLevel}";
        itemIcon.sprite = equipment.icon;
        upgradeButton.GetComponent<TextMeshProUGUI>().text = $"Upgrade: {equipment.upgradeCost}";
    }
    public void SetButtonUpgrade(Equipment equipment)
    {
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => UpdateEquipment(equipment));
    }
}
