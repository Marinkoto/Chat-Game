using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSfx : MonoBehaviour, ISoundPlayer
{
    public AudioSource Source { get; set; }
    private void OnEnable()
    {
        CharacterDataManager.OnCharacterUpgrade.AddListener(() => AudioManager.instance.PlaySound("Upgrade", Source, true));
        EquipmentDataManager.OnEquipmentUpgrade.AddListener(() => AudioManager.instance.PlaySound("Upgrade", Source, true));
    }

    private void Start()
    {
        InitializeSoundPlayer();
    }

    public void InitializeSoundPlayer()
    {
        Source = gameObject.AddComponent<AudioSource>();
    }
}
