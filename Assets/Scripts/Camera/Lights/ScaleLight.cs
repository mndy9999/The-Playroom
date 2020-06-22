using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            transform.localScale += Vector3.one * 5f * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.localScale -= Vector3.one * 5f * Time.deltaTime;

        if (transform.localScale.x < 0.5)
            transform.localScale = Vector3.one * 0.5f;
        if (transform.localScale.x > 2)
            transform.localScale = Vector3.one * 2f;
    }
}
