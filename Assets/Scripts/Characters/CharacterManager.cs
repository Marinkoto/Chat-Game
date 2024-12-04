using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using System;

public class CharacterManager : MonoBehaviour
{
    [Header("Main Character Display")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI statsText;

    [Header("Character Data")]
    [SerializeField] private List<CharacterData> characters;
    [SerializeField] public CharacterData selectedCharacter;
    [SerializeField] TextMeshProUGUI upgradeButton;

    [Header("Carousel")]
    [SerializeField] Image nextImage;
    [SerializeField] Image prevImage;
    [SerializeField] Image mainImage;

    public static CharacterManager instance;

    private int currentIndex = 0;
    private void OnDisable()
    {
        SavingSystem.SaveAllCharacters(characters);
    }
    private void OnEnable()
    {
        LoadingSystem.LoadAllCharacters(characters);
    }

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

    void Start()
    {
        UpdateCarousel();

        DontDestroyOnLoad(gameObject);
    }

    public void SetupCharacterSelection(Button[] characterOptions)
    {
        for (int i = 0; i < characterOptions.Length; i++)
        {
            int index = i;
            characterOptions[i].onClick.AddListener(() => SelectCharacter(index));
        }
    }
    private void ShowStats(CharacterData currentCharacter)
    {
        statsText.text = $"{currentCharacter.name}\n" +
            $"Health: {currentCharacter.maxHealth}\n";
        upgradeButton.text = $"Upgrade {currentCharacter.costToUpgrade}";
    }

    private void ManageEquipment(CharacterData currentCharacter)
    {
        EquipmentManager.instance.SetButtonUpgrade(currentCharacter.weapon);
        EquipmentManager.instance.UpdateUI(currentCharacter.weapon);
    }

    private void UpdateCarousel()
    {
        CharacterData currentCharacter = characters[currentIndex];
        ShowStats(currentCharacter);
        ManageEquipment(currentCharacter);
        if (currentCharacter.icon != null)
        {
            mainImage.sprite = currentCharacter.icon;
            iconImage.sprite = currentCharacter.icon;
        }

        int previousIndex = (currentIndex - 1 + characters.Count) % characters.Count;
        int nextIndex = (currentIndex + 1) % characters.Count;

        //Sets the image of the previous character in the carousel
        CharacterData previousCharacter = characters[previousIndex];
        if (previousCharacter.icon != null)
        {
            prevImage.sprite = previousCharacter.icon;
            prevImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        //Sets the image of the next character in the carousel
        CharacterData nextCharacter = characters[nextIndex];
        if (nextCharacter.icon != null)
        {
            nextImage.sprite = nextCharacter.icon;
            nextImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        mainImage.color = Color.white;
    }

    private CharacterData GetCharacterByIndex(int index)
    {
        return characters[index];
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Count;
        UpdateCarousel();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characters.Count) % characters.Count;
        UpdateCarousel();
    }
    public void SelectCharacter(int index)
    {
        CharacterData character = characters[index];
        selectedCharacter = character;
        SceneSystem.LoadScene(1);
    }
    public void UpgradeCharacter()
    {
        if (CurrencyManager.HasCurrency(UserManager.instance.Data, characters[currentIndex].costToUpgrade))
        {
            CurrencyManager.RemoveCurrency(characters[currentIndex].costToUpgrade, UserManager.instance.Data);
            characters[currentIndex].maxHealth += 1;
            UserManager.instance.Data.combatPower += 1;
            characters[currentIndex].costToUpgrade += 50;
            SavingSystem.SaveCharacter(characters[currentIndex]);
            UpdateCarousel();
        }
    }
}
