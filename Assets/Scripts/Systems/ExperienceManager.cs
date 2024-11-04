using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UserData data;
    
    /// <summary>
    /// Observer pattern following also singleton representation
    /// </summary>
    public static ExperienceManager instance;
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        Initialize();
    }

    public void Initialize()
    {
        data = SavingSystem.LoadPlayerData(UserData.saveKey);
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseLevel()
    {
        if (!CurrencyManager.HasCurrency(data, data.costToLevelUp))
        {
            return;
        }
        CurrencyManager.RemoveCurrency(data.costToLevelUp, data);
        data.level++;
        data.currentExp -= data.expToLevelUp;
        data.expToLevelUp += Mathf.RoundToInt(data.expToLevelUp * 0.5f);
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}
