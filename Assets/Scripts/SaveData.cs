using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int sceneIndex;
    public float[] playerPosition;
    public float[] roomPosition;
    public float[] roomRotation;
    public float[] roomScale;

    public SaveData(Transform playerPos, Transform room)
    {
        sceneIndex = GameSceneManager.Instance.currentScene.transform.GetSiblingIndex();

        playerPosition = new float[3];
        playerPosition[0] = playerPos.position.x;
        playerPosition[1] = playerPos.position.y;
        playerPosition[2] = playerPos.position.z;

        roomPosition = new float[3];
        roomPosition[0] = room.position.x;
        roomPosition[1] = room.position.y;
        roomPosition[2] = room.position.z;

        roomRotation = new float[4];
        roomRotation[0] = room.rotation.x;
        roomRotation[1] = room.rotation.y;
        roomRotation[2] = room.rotation.z;
        roomRotation[3] = room.rotation.w;

        roomScale = new float[3];
        roomScale[0] = room.localScale.x;
        roomScale[1] = room.localScale.y;
        roomScale[2] = room.localScale.z;
    }

}
