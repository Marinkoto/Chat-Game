using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUIManager : MonoBehaviour
{
    [Header("Main Character Display")]
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] TextMeshProUGUI upgradeButton;

    [Header("Carousel")]
    [SerializeField] Image nextImage;
    [SerializeField] Image prevImage;
    [SerializeField] Image mainImage;

    public static int CurrentCharacterIndex { get; set; } = 0;

    private void OnEnable()
    {
        CharacterDataManager.OnCharacterUpgrade += UpdateCarousel;
    }
    private void OnDisable()
    {
        CharacterDataManager.OnCharacterUpgrade -= UpdateCarousel;
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
        CharacterData character = CharacterDataManager.instance.characters[index];
        CharacterDataManager.instance.selectedCharacter = character;
        SceneSystem.LoadScene(1);
    }

    void Start()
    {
        UpdateCarousel();
    }

    public void UpdateCarousel()
    {
        CharacterData currentCharacter = GetCharacterByIndex(CurrentCharacterIndex);
        ShowStats(currentCharacter);
        ManageEquipment(currentCharacter);
        if (currentCharacter.icon != null)
        {
            mainImage.sprite = currentCharacter.icon;
            iconImage.sprite = currentCharacter.icon;
        }

        int previousIndex = (CurrentCharacterIndex - 1 + CharacterDataManager.instance.characters.Count) % CharacterDataManager.instance.characters.Count;
        int nextIndex = (CurrentCharacterIndex + 1) % CharacterDataManager.instance.characters.Count;

        CharacterData previousCharacter = GetCharacterByIndex(previousIndex);
        if (previousCharacter.icon != null)
        {
            prevImage.sprite = previousCharacter.icon;
            prevImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        CharacterData nextCharacter = GetCharacterByIndex(nextIndex);
        if (nextCharacter.icon != null)
        {
            nextImage.sprite = nextCharacter.icon;
            nextImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        mainImage.color = Color.white;
    }

    private void ShowStats(CharacterData currentCharacter)
    {
        statsText.text = $"{currentCharacter.name}\n" +
            $"Health: {currentCharacter.maxHealth}\n";
        upgradeButton.text = $"Upgrade {currentCharacter.costToUpgrade}";
    }

    private CharacterData GetCharacterByIndex(int index)
    {
        return CharacterDataManager.instance.characters[index];
    }

    private void ManageEquipment(CharacterData currentCharacter)
    {
        EquipmentManager.instance.SetButtonUpgrade(currentCharacter.weapon);
        EquipmentManager.instance.UpdateUI(currentCharacter.weapon);
    }
    /// <summary>
    /// Both methods are used for arrow buttons on the menu to change characters
    /// </summary>
    public void NextCharacter()
    {
        CurrentCharacterIndex = (CurrentCharacterIndex + 1) % CharacterDataManager.instance.characters.Count;
        UpdateCarousel();
    }

    public void PreviousCharacter()
    {
        CurrentCharacterIndex = (CurrentCharacterIndex  - 1 + CharacterDataManager.instance.characters.Count) % CharacterDataManager.instance.characters.Count;
        UpdateCarousel();
    }
}
