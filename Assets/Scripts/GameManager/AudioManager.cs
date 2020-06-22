using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AudioManager : MonoBehaviour
{

    private static AudioManager mInstance;
    public static AudioManager Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = FindObjectOfType<AudioManager>();
            return mInstance;
        }

    }

    public Sound[] sounds;

    public float Volume = 5f;


    private void Awake()
    {
        var go = FindObjectsOfType<AudioManager>();
        if (go.Length > 1)
            DestroyImmediate(gameObject);
    }

    private void Start()
    {
        foreach (var s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.loop = s.Loop;
            s.Source.volume = s.Volume;
        }
        UpdateSoundsVolume(Volume);
        PlayMenuTheme();

    }

    public void PlayMenuTheme()
    {
        foreach (var s in sounds)
        {
            s.Source.Stop();
        }
        Play("MenuTheme");
    }

    public void PlayMainTheme()
    {
        foreach (var s in sounds)
        {
            s.Source.Stop();
        }
        Play("MainTheme");
    }

    public void PlayBattleTheme()
    {
        foreach (var s in sounds)
        {
            s.Source.Stop();
        }
        Play("BattleTheme");
    }

    public void PlayOutroTheme()
    {
        foreach (var s in sounds)
        {
            s.Source.Stop();
        }
        Play("OutroTheme");
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if(s != null)
            s.Source.Play();
    }

    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s != null)
            s.Source.Stop();
    }

    public void UpdateSoundsVolume(float volume)
    {
        Volume = volume;
        foreach(var s in sounds)
        {
            s.Source.volume = volume * 0.01f;
        }
    }

}
