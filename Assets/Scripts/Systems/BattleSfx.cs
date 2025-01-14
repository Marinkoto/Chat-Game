using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleSfx : MonoBehaviour, ISoundPlayer
{
    public AudioSource Source { get; set; }
    public AudioSource GameSource { get; set; }
    private void OnEnable()
    {
        ChatManager.OnMessageSent.AddListener(() => AudioManager.Instance.PlaySound("Message", Source, false));
        EnemyBattleHandler.OnEnemiesDead.AddListener(() => AudioManager.Instance.PlaySound("Game Win", GameSource, false));
        PlayerHealth.OnDeath.AddListener(() => AudioManager.Instance.PlaySound("Game Lose", GameSource, false));
    }
    private void Start()
    {
        InitializeSoundPlayer();
    }
    public void InitializeSoundPlayer()
    {
        Source = this.AddComponent<AudioSource>();
        Source.volume = 0.05f;
        GameSource = this.AddComponent<AudioSource>();
        GameSource.volume = 0.3f;
    }
}
