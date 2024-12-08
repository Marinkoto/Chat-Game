using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audios")]
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] public AudioSource buttonSource;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        buttonSource = this.AddComponent<AudioSource>();
    }
    public void PlaySound(string soundName, AudioSource source, bool randomisePitch)
    {
        AudioClip sound = GetSoundByName(soundName);
        if (source != null && source.isActiveAndEnabled)
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
