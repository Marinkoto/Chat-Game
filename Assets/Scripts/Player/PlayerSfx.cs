using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

public class PlayerSfx : MonoBehaviour, ISoundPlayer
{
    public AudioSource Source { get; set; }
    public AudioSource HealthSource { get; set; }
    private void OnEnable()
    {
        EventUtils.AddListeners(new Dictionary<UnityEvent, Action>
        {
            { PlayerHealth.OnHealthChange, () => AudioManager.instance.PlaySound("Heal", HealthSource, false) },
            { PlayerHealth.OnHit, () => AudioManager.instance.PlaySound("Hit", HealthSource, true) },
            { PlayerBattleHandler.OnTimerTick, () => AudioManager.instance.PlaySound("Timer Tick", Source, true) },
            { PlayerBattleHandler.OnTimerEnd, () => AudioManager.instance.PlaySound("Timer End", Source, false) }
        });
    }
    private void Start()
    {
        InitializeSoundPlayer();
        AudioManager.instance.MusicSource.volume = 0.05f;
    }
    public void InitializeSoundPlayer()
    {
        Source = this.AddComponent<AudioSource>();
        Source.volume = 0.75f;
        HealthSource = this.AddComponent<AudioSource>();
    }
}
