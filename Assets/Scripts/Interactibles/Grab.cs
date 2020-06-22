using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : Interactable
{
    public void Start()
    {
        IsInstant = true;
    }
    Transform player;
    public override void Interact(Transform obj)
    {
        player = obj;
        player.GetComponent<PlayerMovementController>().Climbing = true;
    }

    public override void Disinteract()
    {
        player.GetComponent<PlayerMovementController>().Climbing = false;
    }
}
