using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using System;
using UnityEngine.Events;

public class CharacterDataManager : MonoBehaviour
{
    [Header("Character Data")]
    [SerializeField] public List<CharacterData> characters;
    [SerializeField] public CharacterData selectedCharacter;

    public static CharacterDataManager instance;
    public static event Action OnCharacterUpgrade;

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
        DontDestroyOnLoad(gameObject);
    }

    public void UpgradeCharacter()
    {
        if (CurrencyManager.HasCurrency(UserManager.instance.Data, characters[CharacterUIManager.CurrentCharacterIndex].costToUpgrade))
        {
            CurrencyManager.RemoveCurrency(characters[CharacterUIManager.CurrentCharacterIndex].costToUpgrade, UserManager.instance.Data);
            characters[CharacterUIManager.CurrentCharacterIndex].maxHealth += 1;
            UserManager.instance.Data.combatPower += 1;
            characters[CharacterUIManager.CurrentCharacterIndex].costToUpgrade += 50;
            SavingSystem.SaveCharacter(characters[CharacterUIManager.CurrentCharacterIndex]);
            OnCharacterUpgrade?.Invoke();
        }
    }
}
