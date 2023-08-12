using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public string volumenParameterName;
    public string clickSoundName;
    public AudioMixer audioMixer;
    public float defaultVolume;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    
    private Image image_;
    
    private void Start()
    {
        image_ = GetComponent<Image>();
        SetImage();
    }

    private void SetImage()
    {
        audioMixer.GetFloat(volumenParameterName, out var currentVolume);
        if (currentVolume < defaultVolume-1)
        {
            image_.sprite = soundOffSprite;
        }
        else
        {
            image_.sprite = soundOnSprite;
        }
    }
    
    public void ToggleSound()
    {
        audioMixer.GetFloat(volumenParameterName, out var currentVolume);
        if (currentVolume < defaultVolume-1)
        {
            audioMixer.SetFloat(volumenParameterName, defaultVolume);
            AudioManager.instance.PlaySound(clickSoundName);
        }
        else
        {
            audioMixer.SetFloat(volumenParameterName, -80f);
        }
        SetImage();
    }
    
}
