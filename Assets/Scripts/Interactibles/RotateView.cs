using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateView : Interactable
{
    public Transform viewToRotate;
    private Animator playerAnimator;

    private bool interacting;

    public override void Interact(Transform obj)
    {
        interacting = true;

        playerAnimator = obj.GetComponentInChildren<Animator>();
        playerAnimator.SetBool("spinning", true);
    }

    public override void Disinteract()
    {
        interacting = false;
        playerAnimator.SetBool("spinning", false);

    }

    // Start is called before the first frame update
    void Start()
    {
        LocksPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacting)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                viewToRotate.Rotate(Vector3.forward, 10 * Time.deltaTime * 2.0f);
            }
            else if (Input.GetKey(KeyCode.X))
            {
                viewToRotate.Rotate(Vector3.forward, -10 * Time.deltaTime * 2.0f);
            }
        }
    }
}
