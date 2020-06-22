using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBoxAxis : MonoBehaviour
{
    // This script was made to activate an objects X, Y axis movement and Z rotation when the object on which the script is Activates.

    public GameObject toActivateObject;
    public bool xAxis, yAxis, zRotation;
    public float gravityScale;
    private Rigidbody2D myRigidbody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = toActivateObject.GetComponent<Rigidbody2D>();
        myRigidbody.constraints = RigidbodyConstraints2D.None;
        if (!xAxis)
        {
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (!yAxis)
        {
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        if (!zRotation)
        {
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if(gravityScale != 0)
            myRigidbody.gravityScale = gravityScale;
    }
}
