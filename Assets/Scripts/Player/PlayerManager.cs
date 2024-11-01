using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] public PlayerData data;

    private void OnDisable()
    {
        SavingSystem.SavePlayerData(data);

        ExperienceManager.instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void OnEnable()
    {
        data = SavingSystem.LoadPlayerData(PlayerData.saveKey);

        ExperienceManager.instance.OnExperienceChange += HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience)
    {
        data.currentExp += newExperience;
    }
}
