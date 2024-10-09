using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

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

    private int currentIndex = 0;

    void Start()
    {
        if (characters.Count > 0)
        {
            UpdateCarousel();
        }
    }

    private void UpdateCarousel()
    {
        CharacterData currentCharacter = characters[currentIndex];

        statsText.text = $"{currentCharacter.name}\n" +
            $"Health: {currentCharacter.maxHealth}\n" +
            $"Combat Power";

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
            previousImage.transform.localScale = new Vector3(0.5f, 0.5f);
        }

        CharacterData nextCharacter = characters[nextIndex];
        if (nextCharacter.icon != null)
        {
            nextImage.sprite = nextCharacter.icon;
            nextImage.color = new Color(1f, 1f, 1f, 0.5f);
            nextImage.transform.localScale = new Vector3(0.5f, 0.5f);
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
        CharacterData selectedCharacter = characters[index];
        //TODO
    }
}
