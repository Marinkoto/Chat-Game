using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] public UserData data;

    public static UserManager instance;
    private void Awake()
    {
        data = SavingSystem.LoadPlayerData(UserData.saveKey);
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void OnDisable()
    {
        SavingSystem.SavePlayerData(data);

        ExperienceManager.instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void Start()
    {
        ExperienceManager.instance.OnExperienceChange += HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience)
    {
        data.currentExp += newExperience;
    }
}
