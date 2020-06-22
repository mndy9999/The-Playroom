using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public Interactable interactable;
    private PlayerMovementController playerController;
    public LayerMask interactibleLayers;

    private void Start()
    {
        playerController = GetComponent<PlayerMovementController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!(interactibleLayers == (interactibleLayers | (1 << collision.gameObject.layer))) || CinematicsManager.Instance.CinematicsStarted)
            return;

        interactable = collision.GetComponent<Interactable>();
        if (interactable != null && interactable.IsInstant)
            interactable.Interact(transform);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!(interactibleLayers == (interactibleLayers | (1 << collision.gameObject.layer))))
            return;

        if (interactable != null && interactable.IsInstant)
            interactable.Disinteract();
        interactable = null;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !CinematicsManager.Instance.CinematicsStarted)
        {
            if (interactable != null && !interactable.IsInstant)
            {
                if (interactable.LocksPlayer && playerController.PlayerLocked)
                {                  
                    playerController.PlayerLocked = false;
                    interactable.Disinteract();
                }
                else
                {
                    if (interactable.LocksPlayer)
                        playerController.PlayerLocked = true;
                    interactable.Interact(transform);
                }
                
            }
        }

    }
}
