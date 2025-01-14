using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.TextCore.Text;

public class SavingSystem
{
    public static readonly string ENCRYPTION_KEY = "AU!JH*a91BbanU%1Pah&ya81";
    public static void SaveCharacter(CharacterData character)
    {
        string json = JsonUtility.ToJson(character);
        string encryptedJson = EncryptionSystem.Encrypt(json, ENCRYPTION_KEY);
        File.WriteAllText(Application.persistentDataPath + "/" + character.name + ".json", encryptedJson);
        SaveEquipment(character.weapon);
    }
    public static void SavePlayerData(UserData userData)
    {
        string json = JsonUtility.ToJson(userData);
        string encryptedJson = EncryptionSystem.Encrypt(json, ENCRYPTION_KEY);
        File.WriteAllText(Application.persistentDataPath + "/" + UserData.SAVE_KEY + ".json", encryptedJson);;
    }
    public static void SaveEquipment(Equipment equipment)
    {
        string json = JsonUtility.ToJson(equipment);
        string encryptedJson = EncryptionSystem.Encrypt(json, ENCRYPTION_KEY);
        File.WriteAllText(Application.persistentDataPath + "/" + equipment.name + ".json", encryptedJson);
    }
    public static void SaveAllCharacters(List<CharacterData> characters)
    {
        foreach (CharacterData character in characters)
        {
            SaveCharacter(character);
        }
    }
}
