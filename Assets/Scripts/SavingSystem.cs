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
            return JsonUtility.FromJson<CharacterData>(decryptedJson);
        }
        return null;
    }

}
