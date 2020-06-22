using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactable
{
    public Transform teleportTo;
    void Start()
    {
        IsInstant = true;
    }

    public override void Interact(Transform obj)
    {
        if(teleportTo != null)
        {
            obj.position = teleportTo.GetChild(0).position;
        }

    }

}
