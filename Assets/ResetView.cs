using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetView : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;

    void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }

    public void ResetTransform(Vector3 pos, Quaternion rot, Vector3 scl)
    {
        transform.position = pos;
        transform.rotation = rot;
        transform.localScale = scl;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }

    public Vector3 GetScale()
    {
        return scale;
    }

}
