using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class LoadingSystem
{
    public static CharacterData LoadCharacter(string characterName)
    {
        string path = Application.persistentDataPath + "/" + characterName + ".json";
        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = EncryptionSystem.Decrypt(encryptedJson, SavingSystem.ENCRYPTION_KEY);
            CharacterData character = JsonUtility.FromJson<CharacterData>(decryptedJson);
            character.icon = Resources.Load<Sprite>(character.iconPath);
            character.phrases = Resources.LoadAll<Phrase>($"Phrases/{characterName}").ToList();
            character.weapon = LoadEquipment(character.weapon);
            return character;
        }
        else
        {
            return null;
        }
    }
    public static UserData LoadUserData(string key)
    {
        string path = Application.persistentDataPath + "/" + key + ".json";
        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = EncryptionSystem.Decrypt(encryptedJson, SavingSystem.ENCRYPTION_KEY);
            UserData playerData = JsonUtility.FromJson<UserData>(decryptedJson);
            return playerData;
        }
        MenuManager.instance.UpdateGamePanel();
        return new UserData();
    }

    public static Equipment LoadEquipment(Equipment equipment)
    {
        string path = Application.persistentDataPath + "/" + equipment.name + ".json";
        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = EncryptionSystem.Decrypt(encryptedJson,SavingSystem.ENCRYPTION_KEY);
            Equipment loadedEquipment = JsonUtility.FromJson<Equipment>(decryptedJson);
            loadedEquipment.icon = Resources.Load<Sprite>(loadedEquipment.iconPath);
            return loadedEquipment;
        }
        return new Equipment();
    }
    public static void LoadAllCharacters(List<CharacterData> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            CharacterData loadedCharacter = LoadCharacter(characters[i].name);
            if (loadedCharacter != null)
            {
                characters[i] = loadedCharacter;
            }
        }
    }
}
