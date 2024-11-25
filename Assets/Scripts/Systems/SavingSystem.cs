using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.TextCore.Text;

public class SavingSystem
{
    public static readonly string ENCRYPTIONKEY = "AU!JH*a91BbanU%1Pah&ya81";
    public static void SaveCharacter(CharacterData character)
    {
        string json = JsonUtility.ToJson(character);
        string encryptedJson = EncryptionSystem.Encrypt(json, ENCRYPTIONKEY);
        File.WriteAllText(Application.persistentDataPath + "/" + character.name + ".json", encryptedJson);
        
    }
    public static void SavePlayerData(UserData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        string encryptedJson = EncryptionSystem.Encrypt(json, ENCRYPTIONKEY);
        File.WriteAllText(Application.persistentDataPath + "/" + UserData.saveKey + ".json", encryptedJson);;
    }
    public static void SaveAllCharacters(List<CharacterData> characters)
    {
        foreach (CharacterData character in characters)
        {
            SaveCharacter(character);
        }
    }
}
