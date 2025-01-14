using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentDataManager : Singleton<EquipmentDataManager>
{
    public static UnityEvent OnEquipmentUpgrade = new UnityEvent();

    public void UpdateEquipment(Equipment equipment, UserData data)
    {
        if (CurrencyManager.HasCurrency(data, equipment.upgradeCost) && equipment.IsMaxLevel() == false)
        {
            CurrencyManager.RemoveCurrency(equipment.upgradeCost, data);
            equipment.upgradeCost += 250;
            equipment.level++;
            data.combatPower += equipment.combatPowerPerUpgrade;
            ExperienceManager.Instance.AddExperience(Random.Range(50, 100));
            OnEquipmentUpgrade?.Invoke();
            EquipmentUIManager.Instance.UpdateUI(equipment);
        }
    }
}
