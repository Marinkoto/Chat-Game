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

    [Header("Character Data")]
    [SerializeField] PlayerManager playerManager;

    [Header("Character Selection")]
    [SerializeField] Button[] characterOptions;

    [Header("Carousel")]
    [SerializeField] Image nextImage;
    [SerializeField] Image prevImage;
    [SerializeField] Image mainImage;

    public static CharacterManager instance;

    private int currentIndex = 0;
    private void OnDisable()
    {
        foreach (var character in characters)
        {
            SavingSystem.SaveCharacter(character);
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        for (int i = 0; i < characterOptions.Length; i++)
        {
            int index = i;
            characterOptions[i].onClick.AddListener(() => SelectCharacter(index));
        }
        if (characters.Count > 0)
        {
            SavingSystem.LoadAllCharacters(characters);

            UpdateCarousel();
        }
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateStats(CharacterData currentCharacter)
    {
        statsText.text = $"{currentCharacter.name}\n" +
            $"Health: {currentCharacter.maxHealth}\n" +
            $"Combat Power: {currentCharacter.combatPower}";
    }

    private void UpdateCarousel()
    {
        CharacterData currentCharacter = characters[currentIndex];
        UpdateStats(currentCharacter);
        if (currentCharacter.icon != null)
        {
            mainImage.sprite = currentCharacter.icon;
            iconImage.sprite = currentCharacter.icon;
        }

        int previousIndex = (currentIndex - 1 + characters.Count) % characters.Count;
        int nextIndex = (currentIndex + 1) % characters.Count;

        CharacterData previousCharacter = characters[previousIndex];
        if (previousCharacter.icon != null)
        {
            prevImage.sprite = previousCharacter.icon;
            prevImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        CharacterData nextCharacter = characters[nextIndex];
        if (nextCharacter.icon != null)
        {
            nextImage.sprite = nextCharacter.icon;
            nextImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        mainImage.color = Color.white;
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
        if (!CurrencyManager.HasCurrency(playerManager.data, 50))
        {
            return;
        }
        CurrencyManager.RemoveCurrency(50, playerManager.data);
        characters[currentIndex].maxHealth += 1;
        characters[currentIndex].combatPower += 1;
        SavingSystem.SaveCharacter(characters[currentIndex]);
        UpdateCarousel();
    }
}
