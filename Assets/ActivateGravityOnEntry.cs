using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGravityOnEntry : MonoBehaviour
{
    public Vector2 enabledAxis;
    public float gravityScale;
    public bool freezeRotation;
    public float originalGravityScale;

    public Rigidbody2D[] targets;

    public void Start()
    {
        originalGravityScale = targets[0].gravityScale;
    }

    private void OnEnable()
    {
        foreach(var t in targets)
        {
            t.constraints = RigidbodyConstraints2D.None;

            if(enabledAxis.x == 0)
                t.constraints = RigidbodyConstraints2D.FreezePositionX;
            if (enabledAxis.y == 0)
                t.constraints = RigidbodyConstraints2D.FreezePositionY;
            if (freezeRotation)
                t.constraints = RigidbodyConstraints2D.FreezeRotation;

            t.gravityScale = gravityScale;
        }
    }

    private void OnDisable ()
    {
        foreach (var t in targets)
        {
            t.constraints = RigidbodyConstraints2D.FreezeAll;

            t.gravityScale = originalGravityScale;
        }
    }


}
