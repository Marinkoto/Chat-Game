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

    public UserData Data { get; set; }

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
        Data = UserManager.instance.Data;
        SetupExitButtons();
        CharacterManager.instance.SetupCharacterSelection(characterOptions);
    }
    public void UpdateUserStats()
    {
        userStats.text = $"Level: {Data.level}\n" +
            $"Combat Power: {Data.combatPower}\n" +
            $"Currency: {Data.currency}\n";
    }
    private void SetupExitButtons()
    {
        foreach (var button in exitButtons)
        {
            button.onClick.AddListener(() => SceneSystem.ExitGame());
        }
    }
    public void UpdateGamePanel()
    {
        updatePanel.SetActive(true);
    }
}
