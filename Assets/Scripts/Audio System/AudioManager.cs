using System;
using Benetti;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer mixer;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.Loop;
            s.source.outputAudioMixerGroup = s.MixerGroup;
        }
    }

    public static AudioManager instance;
    void Start()
    {
        // Ensure there's only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager instance created");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate AudioManager instance destroyed");
            return;
        }
    }
    public void PlaySound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.source.Play();
    }
    public void StopSound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name); 
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "is not found!");
        }
        s.source.Stop();
    }
}
