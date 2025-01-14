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
    [HideInInspector] TextMeshProUGUI advanceButtonText;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI combatPowerText;

    private void Start()
    {
        TickManager.Instance.RegisterTimedTick(1f, SetUI);
        advanceButtonText = advanceButton.GetComponent<TextMeshProUGUI>();
    }
    private void OnDisable()
    {
        TickManager.Instance.UnregisterTimedTick(SetUI);
    }

    private void OnEnable()
    {
        advanceButton.onClick.AddListener(() => ExperienceManager.Instance.IncreaseLevel(UserManager.Instance.data));
        advanceButton.onClick.AddListener(() => SavingSystem.SavePlayerData(UserManager.Instance.data));
    }
    public void Update()
    {
        SliderUtils.SmoothAnimation(expBar, 2, UserManager.Instance.data.currentExp);
    }
    private void SetUI()
    {
        combatPowerText.text = $"{UserManager.Instance.data.combatPower} CP";
        advanceButtonText.text = $"Advance {UserManager.Instance.data.costToLevelUp}";
        statsText.text = $"Level : {UserManager.Instance.data.level}/{UserManager.Instance.data.maxLevel}\n" +
            $"Wins: {UserManager.Instance.data.wins}\n" +
            $"Combat Power: {UserManager.Instance.data.combatPower}";
        expText.text = $"{UserManager.Instance.data.currentExp}/{UserManager.Instance.data.expToLevelUp}";
        SliderUtils.SetupSlider(expBar, UserManager.Instance.data.currentExp, UserManager.Instance.data.expToLevelUp);
    }
}
