using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, IInitializable
{
    public float MasterVolume
    {
        get 
        {
            float volume;
            audioMixer.GetFloat("Master", out volume);
            return volume;
        }
        set
        {
            if(value == 0f)
            {
                audioMixer.SetFloat("Master", -80f);
            }
            else
            {
                audioMixer.SetFloat("Master", -40f + value * 20f);
            }
        }
    }
    public float BGMVolume
    {
        get
        {
            float volume;
            audioMixer.GetFloat("BGM", out volume);
            return volume;
        }
        set
        {
            if (value == 0f)
            {
                audioMixer.SetFloat("BGM", -80f);
            }
            else
            {
                audioMixer.SetFloat("BGM", -40f + value * 20f);
            }
        }
    }
    public float SFXVolume
    {
        get
        {
            float volume;
            audioMixer.GetFloat("SFX", out volume);
            return volume;
        }
        set
        {
            if (value == 0f)
            {
                audioMixer.SetFloat("SFX", -80f);
            }
            else
            {
                audioMixer.SetFloat("SFX", -40f + value * 20f);
            }
        }
    }

    AudioMixer audioMixer;

    public void Initialize()
    {
        audioMixer = GameManager.Resource.Load<AudioMixer>("Audio/AudioMixer");
    }
}
