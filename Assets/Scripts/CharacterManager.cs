using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using System;

public class CharacterManager : MonoBehaviour
{
    [Header("Main Character Display")]
    [SerializeField] private Image mainImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI statsText;

    [Header("Faded Character Icons")]
    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    [Header("Character Data")]
    [SerializeField] private List<CharacterData> characters;
    [SerializeField] public CharacterData selectedCharacter;

    [Header("Character Selection")]
    [SerializeField] Button[] characterOptions;

    public static CharacterManager instance;

    private int currentIndex = 0;
    private void OnDisable()
    {
        SavingSystem.SaveCharacter(characters[currentIndex]);
    }

    private void Awake()
    {
        if(instance == null)
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
            UpdateCarousel();
        }
        SavingSystem.LoadCharacter(characters[currentIndex].name);
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateStats(CharacterData currentCharacter)
    {
        statsText.text = $"{currentCharacter.name}\n" +
            $"Health: {currentCharacter.maxHealth}\n" +
            $"Combat Power";
    }

    private void UpdateCarousel()
    {
        CharacterData currentCharacter = characters[currentIndex];

        currentCharacter = SavingSystem.LoadCharacter(currentCharacter.name);
        currentCharacter ??= characters[currentIndex];
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
            previousImage.sprite = previousCharacter.icon;
            previousImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        CharacterData nextCharacter = characters[nextIndex];
        if (nextCharacter.icon != null)
        {
            nextImage.sprite = nextCharacter.icon;
            nextImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        mainImage.color = Color.white;
        SavingSystem.SaveCharacter(currentCharacter);
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
        characters[currentIndex].maxHealth += 1;
        SavingSystem.SaveCharacter(characters[currentIndex]);
        UpdateCarousel();
    }
}
