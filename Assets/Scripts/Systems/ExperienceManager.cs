using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : Singleton<ExperienceManager>
{
    //Observer pattern following also singleton representation
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;

    public void IncreaseLevel(UserData data)
    {
        if(data.currentExp >= data.expToLevelUp)
        {
            if (CurrencyManager.HasCurrency(data, data.costToLevelUp))
            {
                CurrencyManager.RemoveCurrency(data.costToLevelUp, data);
                data.level++;
                data.combatPower += 5;
                data.currentExp -= data.expToLevelUp;
                data.costToLevelUp += Mathf.RoundToInt(data.level * 100);
                data.expToLevelUp += Mathf.RoundToInt(data.expToLevelUp * 0.5f);
            }
        }
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}
