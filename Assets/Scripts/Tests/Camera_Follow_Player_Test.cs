using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player_Test : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
