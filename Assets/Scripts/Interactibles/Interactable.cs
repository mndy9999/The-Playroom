using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    //do not edit these in the inspector
    [HideInInspector]
    public bool IsInstant;
    [HideInInspector]
    public bool LocksPlayer;

    public virtual void Interact(Transform obj)
    {

    }

    public virtual void Disinteract()
    {

    }

}
