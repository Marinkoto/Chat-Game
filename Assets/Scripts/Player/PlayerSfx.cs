using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSfx : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource source;

    private void OnEnable()
    {
        PlayerHealth.OnHit += () => AudioManager.instance.PlaySound("Player Hit", source, true);
    }
    private void OnDisable()
    {
        PlayerHealth.OnHit -= () => AudioManager.instance.PlaySound("Player Hit", source, true);
    }

    private void Awake()
    {
        source = this.AddComponent<AudioSource>();
    }

}
