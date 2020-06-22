using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEventsWhenEnabled : MonoBehaviour
{
    public bool Played;
    public Cinematic[] cinematicEvents;

    private void OnEnable()
    {
        if (!Played)
        {
            CinematicsManager.Instance.StartCinematics(cinematicEvents);
            Played = true;
        }
    }
}
