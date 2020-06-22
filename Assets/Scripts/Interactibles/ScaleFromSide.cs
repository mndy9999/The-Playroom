using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromSide : Interactable
{
    private bool interacting;

    public Transform viewToScale;

    private Vector2 minScaleOnAxis;
    private Vector2 maxScaleOnAxis;

    enum ChangeAxis { X, Y, Both};
    [SerializeField]
    ChangeAxis ChangeOnAxis;

    public float scaleValue;

    private Animator playerAnimator;

    public override void Interact(Transform obj)
    {
        interacting = true;

        if(ChangeOnAxis == ChangeAxis.X)
        {
            minScaleOnAxis.y = viewToScale.localScale.y;
            maxScaleOnAxis.y = viewToScale.localScale.y;
        }
        else if(ChangeOnAxis == ChangeAxis.Y)
        {
            minScaleOnAxis.x = viewToScale.localScale.x;
            maxScaleOnAxis.x = viewToScale.localScale.x;
        }
        
        playerAnimator = obj.GetComponentInChildren<Animator>();
        playerAnimator.SetBool("spinning", true);

    }
    public override void Disinteract()
    {
        interacting = false;
        playerAnimator.SetBool("spinning", false);
        AudioManager.Instance.StopPlay("Spin");

    }

    private void SetupMargins()
    {
        var newScale = viewToScale.localScale + scaleVector * scaleValue;

        if (ChangeOnAxis == ChangeAxis.X)
        {
            minScaleOnAxis = new Vector3(Mathf.Min(newScale.x, baseScale.x), viewToScale.localScale.y, viewToScale.localScale.z);
            maxScaleOnAxis = new Vector3(Mathf.Max(newScale.x, baseScale.x), viewToScale.localScale.y, viewToScale.localScale.z);
        }
        else if(ChangeOnAxis == ChangeAxis.Y)
        {
            minScaleOnAxis = new Vector3(viewToScale.localScale.x, Mathf.Min(newScale.y, baseScale.y), viewToScale.localScale.z);
            maxScaleOnAxis = new Vector3(viewToScale.localScale.x, Mathf.Max(newScale.y, baseScale.y), viewToScale.localScale.z);
        }
        else
        {
            minScaleOnAxis = new Vector3(Mathf.Min(newScale.x, baseScale.x), Mathf.Min(newScale.y, baseScale.y), viewToScale.localScale.z);
            maxScaleOnAxis = new Vector3(Mathf.Max(newScale.x, baseScale.x), Mathf.Max(newScale.y, baseScale.y), viewToScale.localScale.z);
        }
    }

    private Vector3 scaleVector;
    private Vector3 baseScale;

    public void Start()
    {
        switch (ChangeOnAxis)
        {
            case ChangeAxis.X:
                scaleVector = Vector3.right;
                break;
            case ChangeAxis.Y:
                scaleVector = Vector3.up;
                break;
            case ChangeAxis.Both:
                scaleVector = new Vector3(1, 1, 0);
                break;
        }
        LocksPlayer = true;
        baseScale = viewToScale.localScale;
        SetupMargins();

    }

    public void Update()
    {
        if (interacting)
        {
            if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
                AudioManager.Instance.Play("Spin");
            if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X))
                AudioManager.Instance.StopPlay("Spin");
            if (Input.GetKey(KeyCode.Z))
            {
                
                ScaleMin();
                var wheel = GetComponentInChildren<RotateWheelCircle>();
                if(wheel != null)
                    wheel.transform.Rotate(Vector3.forward, 10 * Time.deltaTime * 10.0f);
                GameSceneManager.Instance.UpdateCameras();
            }
            else if (Input.GetKey(KeyCode.X))
            {
                ScaleMax();
                var wheel = GetComponentInChildren<RotateWheelCircle>();
                if (wheel != null)
                    wheel.transform.Rotate(Vector3.forward, -10 * Time.deltaTime * 10.0f);
                GameSceneManager.Instance.UpdateCameras();
            }
        }
    }

    public void ScaleMin()
    {       
        if(Vector3.Distance(viewToScale.localScale, maxScaleOnAxis) > 0)
        {            
            viewToScale.localScale = Vector3.MoveTowards(viewToScale.localScale, maxScaleOnAxis, Time.deltaTime * .5f);
        }
    }

    public void ScaleMax()
    {
        if (Vector3.Distance(viewToScale.localScale, minScaleOnAxis) > 0)
        {
            viewToScale.localScale = Vector3.MoveTowards(viewToScale.localScale, minScaleOnAxis, Time.deltaTime * .5f);
        }
    }
}
