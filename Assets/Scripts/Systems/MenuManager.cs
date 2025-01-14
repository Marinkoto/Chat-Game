using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [Header("Components")]
    [SerializeField] Button[] exitButtons;
    [SerializeField] Button[] characterOptions;
    [SerializeField] TextMeshProUGUI userStats;
    [SerializeField] GameObject updatePanel;
    [SerializeField] Button urlButton;
    public override void Awake()
    {
        isPersistent = false;
    }
    private void Start()
    {
        SetupExitButtons();
        SetupUrlButton();
        CharacterUIManager.SetupCharacterSelection(characterOptions);
        AudioManager.Instance.MusicSource.volume = 0.2f;
    }
    public void UpdateUserStats()
    {
        userStats.text = $"Level: {UserManager.Instance.data.level}\n" +
            $"Combat Power: {UserManager.Instance.data.combatPower}\n" +
            $"Currency: {UserManager.Instance.data.currency}\n";
    }
    private void SetupExitButtons()
    {
        foreach (var button in exitButtons)
        {
            button.onClick.AddListener(() => SceneSystem.ExitGame());
        }
    }
    private void SetupUrlButton()
    {
        urlButton.onClick.AddListener(() => UrlManager.OpenUrl("https://margata.itch.io/phrase-fighters/rate"));
    }
    public void UpdateGamePanel()
    {
        updatePanel.SetActive(true);
    }
}
