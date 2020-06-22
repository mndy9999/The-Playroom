using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Interactable
{
    private Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform;
        IsInstant = true;
    }

    public override void Interact(Transform obj)
    {
        var controller = obj.GetComponent<PlayerMovementController>();
        controller.Climbing = false;
        controller.PlayerHit = true;

        if (Vector3.Distance(transform.position, startPos.position) > 10)
            DestroyImmediate(gameObject);
    }


}
