using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipmentUIManager : Singleton<EquipmentUIManager>
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI equipmentStats;
    [SerializeField] Button upgradeButton;
    [SerializeField] Image itemIcon;
    public override void Awake()
    {
        isPersistent = false;
    }
    public void UpdateUI(Equipment equipment)
    {
        equipmentStats.text = $"{equipment.name}" +
            $"\nLevel: {equipment.level}/{equipment.maxLevel}\n";
        itemIcon.sprite = equipment.icon;
        if (equipment.IsMaxLevel() == false)
        {
            upgradeButton.GetComponent<TextMeshProUGUI>().text = $"Upgrade: {equipment.upgradeCost}";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.GetComponent<TextMeshProUGUI>().text = $"Maxxed";
            upgradeButton.interactable = false;
        }
    }
    public void SetButtonUpgrade(Equipment equipment)
    {
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => EquipmentDataManager.Instance.UpdateEquipment(equipment,UserManager.Instance.data));
    }
}
