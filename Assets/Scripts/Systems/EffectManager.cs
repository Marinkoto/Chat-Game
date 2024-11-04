using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject[] chatEffects;
    [SerializeField] public Volume chatVolume;
    public static EffectManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    public void ChatEffect()
    {
        Vector3 randPos = new(Random.Range(-5, 5), Random.Range(-3, 5));
        Instantiate(chatEffects[Random.Range(0, chatEffects.Length)], randPos, Quaternion.identity);
    }
}