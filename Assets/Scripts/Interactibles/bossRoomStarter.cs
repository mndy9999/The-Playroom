using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossRoomStarter : MonoBehaviour
{
    public GameObject cup, closeTiles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        closeTiles.SetActive(true);
        cup.SetActive(false); // The script is attached to this object, so this should be the last object to be disabled.
    }
}
