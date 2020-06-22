using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectileDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Proyectile")
        {
            Destroy(collision.gameObject);
        }
    }
}
