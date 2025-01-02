using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserUIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Button advanceButton;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI expText;

    private void OnEnable()
    {
        SetUI();
        advanceButton.onClick.AddListener(() => ExperienceManager.instance.IncreaseLevel(UserManager.instance.data));
        advanceButton.onClick.AddListener(() => SetUI());
        advanceButton.onClick.AddListener(() => SavingSystem.SavePlayerData(UserManager.instance.data));
    }
    public void Update()
    {
        SliderUtils.SmoothAnimation(expBar, 2, UserManager.instance.data.currentExp);
    }
    private void SetUI()
    {
        advanceButton.GetComponent<TextMeshProUGUI>().text = $"Advance {UserManager.instance.data.costToLevelUp}";
        statsText.text = $"Level : {UserManager.instance.data.level}/{UserManager.instance.data.maxLevel}\n" +
            $"Wins: {UserManager.instance.data.wins}\n" +
            $"Combat Power: {UserManager.instance.data.combatPower}";
        expText.text = $"{UserManager.instance.data.currentExp}/{UserManager.instance.data.expToLevelUp}";
        SliderUtils.SetupSlider(expBar, UserManager.instance.data.currentExp, UserManager.instance.data.expToLevelUp);
    }
}
