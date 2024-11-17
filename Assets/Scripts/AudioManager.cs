using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;
    
    [SerializeField] Sound[] _sounds;
    [SerializeField] private float _fadeTime = 0.5f;
    Dictionary<string, AudioClip> audioDict = new();
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        DefineSounds();
    }

    private void DefineSounds()
    {
        foreach (var sound in _sounds)
        {
            audioDict.Add(sound.name, sound.clip);
        }
    }


    public void PlaySFX(string soundName)
    {
        if (audioDict.ContainsKey(soundName))
        {
            _sfxSource.PlayOneShot(audioDict[soundName]);
        }
    }
    
}
