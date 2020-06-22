using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckController : MonoBehaviour
{
    private PlayerMovementController playerController;

    private void Start()
    {
        playerController = transform.root.GetComponent<PlayerMovementController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 12 && collision.gameObject.layer != 11)
            return;
        //Debug.Log("Colliding " + collision.gameObject.name);
        playerController.IsGrounded = true;
    }
}
