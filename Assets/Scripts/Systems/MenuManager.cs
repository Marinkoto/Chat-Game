using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Button[] exitButtons;
    [SerializeField] Button[] characterOptions;
    [SerializeField] TextMeshProUGUI userStats;
    [SerializeField] GameObject updatePanel;
    [SerializeField] Button urlButton;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetupExitButtons();
        SetupUrlButton();
        CharacterUIManager.SetupCharacterSelection(characterOptions);
        AudioManager.instance.MusicSource.volume = 0.2f;
    }
    public void UpdateUserStats()
    {
        userStats.text = $"Level: {UserManager.instance.data.level}\n" +
            $"Combat Power: {UserManager.instance.data.combatPower}\n" +
            $"Currency: {UserManager.instance.data.currency}\n";
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
