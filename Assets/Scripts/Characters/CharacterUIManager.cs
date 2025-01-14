using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUIManager : MonoBehaviour
{
    [Header("Main Character Display")]
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] Button upgradeButton;
    [SerializeField] TextMeshProUGUI upgradeButtonText;
    [SerializeField] TextMeshProUGUI nameText;

    [Header("Carousel")]
    [SerializeField] Image nextImage;
    [SerializeField] Image prevImage;
    [SerializeField] Image mainImage;

    public static int CurrentCharacterIndex { get; set; } = 0;
    
    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(
            () => CharacterDataManager.Instance.UpgradeCharacter(UserManager.Instance.data));
        CharacterDataManager.OnCharacterUpgrade.AddListener(UpdateCarousel);
    }
    void Start()
    {
        UpdateCarousel();
    }
    public static void SetupCharacterSelection(Button[] characterOptions)
    {
        for (int i = 0; i < characterOptions.Length; i++)
        {
            int index = i;
            characterOptions[i].onClick.AddListener(() => SelectCharacter(index));
        }
    }
    public static void SelectCharacter(int index)
    {
        CharacterData character = CharacterDataManager.Instance.characters[index];
        CharacterDataManager.Instance.selectedCharacter = character;
        if (UserManager.Instance.data.tutorialCompleted)
        {
            SceneSystem.LoadScene(1);
        }
        else
        {
            SceneSystem.LoadScene(2);
        }
    }

    public void UpdateCarousel()
    {
        CharacterData currentCharacter = GetCharacterByIndex(CurrentCharacterIndex);
        ManageCharacter(currentCharacter);
        SetIcon(mainImage, currentCharacter.icon, 0.5f);
        SetIcon(iconImage, currentCharacter.icon,1);

        CharacterData previousCharacter = GetCharacterByIndex(GetPreviousCharacter());
        SetIcon(prevImage, previousCharacter.icon,0.5f);

        CharacterData nextCharacter = GetCharacterByIndex(GetNextCharacter());
        SetIcon(nextImage, nextCharacter.icon,0.5f);
        mainImage.color = Color.white;
    }
    private void SetIcon(Image image, Sprite sprite,float alpha)
    {
        if (sprite != null)
        {
            image.sprite = sprite;
            image.color = new Color(1f, 1f, 1f, alpha);
        }
    }
    private void ManageCharacter(CharacterData character)
    {
        EquipmentUIManager.Instance.UpdateUI(character.weapon);
        EquipmentUIManager.Instance.SetButtonUpgrade(character.weapon);
        UpdateStats(character);
    }
    private void UpdateStats(CharacterData currentCharacter)
    {
        nameText.text = $"{currentCharacter.name}  {UserManager.Instance.data.combatPower} CP";
        statsText.text = $"Health: {currentCharacter.maxHealth}\n" +
            $"Level : {currentCharacter.level}/{currentCharacter.maxLevel}";
        if (currentCharacter.IsMaxLevel() == false)
        {
            upgradeButtonText.text = $"Upgrade {currentCharacter.costToUpgrade}";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButtonText.text = $"Maxxed";
            upgradeButton.interactable = false;
        }
    }

    private CharacterData GetCharacterByIndex(int index)
    {
        return ListUtils.GetItemByIndex(CharacterDataManager.Instance.characters, index);
    }
    public void NextCharacter()
    {
        CurrentCharacterIndex = GetNextCharacter();
        UpdateCarousel();
    }

    public void PreviousCharacter()
    {
        CurrentCharacterIndex = GetPreviousCharacter();
        UpdateCarousel();
    }
    public int GetPreviousCharacter()
    {
        return (CurrentCharacterIndex - 1 + CharacterDataManager.Instance.GetCharacterCount()) % CharacterDataManager.Instance.GetCharacterCount();
    }
    public int GetNextCharacter()
    {
        return (CurrentCharacterIndex + 1) % CharacterDataManager.Instance.GetCharacterCount();
    }
}
