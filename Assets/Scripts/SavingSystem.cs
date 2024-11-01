using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.TextCore.Text;

public class SavingSystem
{
    private static readonly string encryptionKey = "AU!JH*a91BbanU%1Pah&ya81";
    public static void SaveCharacter(CharacterData character)
    {
        string json = JsonUtility.ToJson(character);
        string encryptedJson = EncryptionSystem.Encrypt(json, encryptionKey);
        File.WriteAllText(Application.persistentDataPath + "/" + character.name + ".json", encryptedJson);
        
    }
    public static CharacterData LoadCharacter(string characterName)
    {
        string path = Application.persistentDataPath + "/" + characterName + ".json";
        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = EncryptionSystem.Decrypt(encryptedJson, encryptionKey);
            CharacterData character = JsonUtility.FromJson<CharacterData>(decryptedJson);
            character.icon = Resources.Load<Sprite>(character.iconPath);
            return character;
        }
        return null;
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
    public static void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        string encryptedJson = EncryptionSystem.Encrypt(json, encryptionKey);
        File.WriteAllText(Application.persistentDataPath + "/" + PlayerData.saveKey + ".json", encryptedJson);;
    }
    public static PlayerData LoadPlayerData(string key)
    {
        string path = Application.persistentDataPath + "/" + key + ".json";
        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = EncryptionSystem.Decrypt(encryptedJson, encryptionKey);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(decryptedJson);
            return playerData;
        }
        return new PlayerData();
    }
}
