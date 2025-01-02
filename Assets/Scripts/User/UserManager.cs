using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public UserData data;

    public static UserManager instance;
    private void Awake()
    {
        data = LoadingSystem.LoadUserData(UserData.SAVE_KEY);   
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
