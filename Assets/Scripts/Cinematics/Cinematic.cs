using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : ScriptableObject
{

    public float delay;

    public virtual IEnumerator Play()
    {
        yield return null;
    }

    public void GoNext()
    {
        CinematicsManager.Instance.DisplayNextSentence();
    }
}
