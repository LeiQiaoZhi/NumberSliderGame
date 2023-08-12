using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetMusicVolume(float _volume)
    {
        audioMixer.SetFloat("musicVol", _volume);
    }
    
    public void SetSoundFxVolume(float _volume)
    {
        audioMixer.SetFloat("soundFxVol", _volume);
    }
}
