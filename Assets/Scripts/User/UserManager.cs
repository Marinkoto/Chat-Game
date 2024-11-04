using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] public UserData data;

    private void OnDisable()
    {
        SavingSystem.SavePlayerData(data);

        ExperienceManager.instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void OnEnable()
    {
        data = SavingSystem.LoadPlayerData(UserData.saveKey);

        ExperienceManager.instance.OnExperienceChange += HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience)
    {
        data.currentExp += newExperience;
    }
}
