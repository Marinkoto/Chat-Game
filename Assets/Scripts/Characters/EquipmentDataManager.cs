using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentDataManager : MonoBehaviour
{
    public static EquipmentDataManager instance;
    public static UnityEvent OnEquipmentUpgrade = new UnityEvent();

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

    public void UpdateEquipment(Equipment equipment, UserData data)
    {
        if (CurrencyManager.HasCurrency(data, equipment.upgradeCost) && equipment.IsMaxLevel() == false)
        {
            CurrencyManager.RemoveCurrency(equipment.upgradeCost, data);
            equipment.upgradeCost += 250;
            equipment.level++;
            data.combatPower += equipment.combatPowerPerUpgrade;
            ExperienceManager.instance.AddExperience(Random.Range(50, 100));
            OnEquipmentUpgrade?.Invoke();
            EquipmentUIManager.instance.UpdateUI(equipment);
        }
    }
}
