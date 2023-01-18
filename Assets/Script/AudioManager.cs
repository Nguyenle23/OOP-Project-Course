using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    //Call right before
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        //Playing music immediately when the Level1 scene start
        Play("Theme");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //Find sound in Array
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Play();
    }

    public void DestroyMusicCur()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}