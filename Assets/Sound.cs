using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound 
{
    public string Name;

    public AudioClip Clip;

    [Range(0, 1)]
    public float Volume;

    public bool Loop;

    [HideInInspector]
    public AudioSource Source;

}
