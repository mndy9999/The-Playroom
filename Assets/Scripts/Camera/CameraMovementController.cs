using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{

    private Camera mainCam;

    private bool isMoving;
    private bool isRescaling;
    private Vector3 mTargetPos;
    private float mTargetScale;

    public float moveSpeed = 10.0f;

    public Vector3 TargetPos
    {
        set
        {
            mTargetPos = value;
            isMoving = true;
        }

    }

    public float TargetScale
    {
        set
        {
            mTargetScale = value;
            isRescaling = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMoving)
            HandleCameraMove();
        if (isRescaling)
            HandleCameraRescale();
    }

    private void HandleCameraMove()
    {
        if (transform.position != mTargetPos)
            transform.position = Vector3.MoveTowards(transform.position, mTargetPos, Time.deltaTime * moveSpeed);
        else
        {
            isMoving = false;
        }
    }

    private void HandleCameraRescale()
    {
        if (mainCam.orthographicSize != mTargetScale)
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, mTargetScale, Time.deltaTime * 5.0f);
        else
            isRescaling = false;

    }

}
