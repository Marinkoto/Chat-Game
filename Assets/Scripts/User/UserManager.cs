using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserManager : Singleton<UserManager>
{
    public UserData data;
    public override void Awake()
    {
        base.Awake();
        data = LoadingSystem.LoadUserData(UserData.SAVE_KEY);   
    }
    private void OnDisable()
    {
        SavingSystem.SavePlayerData(data);

        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void Start()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience)
    {
        data.currentExp += newExperience;
    }
}
