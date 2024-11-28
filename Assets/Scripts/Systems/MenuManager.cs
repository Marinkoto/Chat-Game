using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Button exitButton;
    [SerializeField] Button[] characterOptions;

    private void Start()
    {
        exitButton.onClick.AddListener(() => SceneSystem.ExitGame());
        CharacterManager.instance.SetupCharacterSelection(characterOptions);
    }
}
