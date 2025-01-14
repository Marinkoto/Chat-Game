using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>,ISoundPlayer
{
    [Header("Audios")]
    [SerializeField] private AudioClip[] sounds;
    public AudioSource Source { get; set; }
    [SerializeField] public AudioSource MusicSource;
    
    private void Start()
    {
        InitializeSoundPlayer();
    }
    public void InitializeSoundPlayer()
    {
        Source = this.AddComponent<AudioSource>();
        Source.volume = 0.75f;
    }
    public void PlaySound(string soundName, AudioSource source, bool randomisePitch)
    {
        AudioClip sound = GetSoundByName(soundName);
        if (source != null && source.isActiveAndEnabled && !source.isPlaying)
        {
            if (randomisePitch)
            {
                SetRandomPitch(source);
            }
            else
            {
                SetDefaultPitch(source);
            }
            source.PlayOneShot(sound);
        }
    }
    private AudioClip GetSoundByName(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == name)
            {
                return sound;
            }
        }
        return null;
    }
    public void SetRandomPitch(AudioSource source)
    {
        source.pitch = Random.Range(0.9f, 1.2f);
    }
    private void SetDefaultPitch(AudioSource source)
    {
        source.pitch = 1;
    }
}
