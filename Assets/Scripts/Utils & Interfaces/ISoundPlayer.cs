using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundPlayer
{
    AudioSource Source { get; set; }
    void InitializeSoundPlayer();
}
