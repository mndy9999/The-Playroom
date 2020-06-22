using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLever : Interactable
{

    public GameObject opensDoor;
    public Sprite closedSprite;

    public Sprite openDoorSprite;

    public override void Interact(Transform obj)
    {
        GetComponent<SpriteRenderer>().sprite = closedSprite;
        GetComponent<Collider2D>().enabled = false;

        opensDoor.GetComponent<AudioSource>().Play();
        opensDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        opensDoor.GetComponent<BoxCollider2D>().enabled = false;

        var newPos = opensDoor.transform.position;
        newPos.y -= 1.5f;
        opensDoor.transform.position = newPos;

        var newRot = opensDoor.transform.rotation;
        newRot.z = 0;
        opensDoor.transform.rotation = newRot;

    }
}
