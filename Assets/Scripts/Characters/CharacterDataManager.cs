using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using System;
using UnityEngine.Events;

public class CharacterDataManager : Singleton<CharacterDataManager>
{
    [Header("Character Data")]
    [SerializeField] public List<CharacterData> characters;
    [SerializeField] public CharacterData selectedCharacter;

    public static UnityEvent OnCharacterUpgrade = new UnityEvent();

    private void OnDisable()
    {
        SavingSystem.SaveAllCharacters(characters);
        OnCharacterUpgrade.RemoveAllListeners();
    }
    private void OnEnable()
    {
        LoadingSystem.LoadAllCharacters(characters);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpgradeCharacter(UserData data)
    {
        CharacterData currentCharacter = characters[CharacterUIManager.CurrentCharacterIndex];
        if (CurrencyManager.HasCurrency(data, currentCharacter.costToUpgrade)
            && currentCharacter.IsMaxLevel() == false)
        {
            CurrencyManager.RemoveCurrency(currentCharacter.costToUpgrade, data);
            currentCharacter.maxHealth += 1;
            data.combatPower += 3;
            currentCharacter.level++;
            currentCharacter.costToUpgrade += 50;
            SavingSystem.SaveCharacter(currentCharacter);
            OnCharacterUpgrade?.Invoke();
            ExperienceManager.Instance.AddExperience(UnityEngine.Random.Range(50, 100));
        }
    }
    public int GetCharacterCount()
    {
        return characters.Count;
    }
}
