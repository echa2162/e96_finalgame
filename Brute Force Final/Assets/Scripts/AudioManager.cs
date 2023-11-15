using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public static AudioManager instance;
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        LoadVolume();

    }

    
    void LoadVolume() //volume saved in VolumeSettings.cs
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY,1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY,1f);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);

    }

}
