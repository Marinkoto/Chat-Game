using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public UserData Data { get; set; }

    public static UserManager instance;
    private void Awake()
    {
        Data = LoadingSystem.LoadUserData(UserData.SAVEKEY);   
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
        SavingSystem.SavePlayerData(Data);

        ExperienceManager.instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void Start()
    {
        ExperienceManager.instance.OnExperienceChange += HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience)
    {
        Data.currentExp += newExperience;
    }
}
