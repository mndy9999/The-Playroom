using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Interactable
{
    private bool interacted = false;
    private void Start()
    {
        IsInstant = true;    
    }

    public override void Interact(Transform obj)
    {
        if (!interacted)
        {
            obj.GetComponent<PlayerMovementController>().PlayerRespawn = true;
            obj.GetComponentInChildren<Animator>().SetBool("hit_spikes", true);
            AudioManager.Instance.Play("Spikes");
            interacted = true;
        }

    }

    public override void Disinteract()
    {
        interacted = false;
    }
}
