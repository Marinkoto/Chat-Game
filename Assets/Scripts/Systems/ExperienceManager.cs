using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    //Observer pattern following also singleton representation
    public static ExperienceManager instance;
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;

    private void Awake()
    {
        Initialize();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseLevel(UserData data)
    {
        if(data.currentExp >= data.expToLevelUp)
        {
            if (CurrencyManager.HasCurrency(data, data.costToLevelUp))
            {
                CurrencyManager.RemoveCurrency(data.costToLevelUp, data);
                data.level++;
                data.currentExp -= data.expToLevelUp;
                data.costToLevelUp += Mathf.RoundToInt(data.level * 150);
                data.expToLevelUp += Mathf.RoundToInt(data.expToLevelUp * 0.5f);
            }
        }
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}
