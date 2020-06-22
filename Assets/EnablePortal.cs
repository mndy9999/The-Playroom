using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePortal : MonoBehaviour
{
    public GameObject Portal;
    // Start is called before the first frame update

    private void Awake()
    {
        Portal = GameObject.FindGameObjectWithTag("LastPortal");
    }

    public void EnableFinalPortal()
    {
        Portal.GetComponent<BoxCollider2D>().enabled = true;
    }
}
