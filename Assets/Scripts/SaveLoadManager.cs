using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager mInstance;
    public static SaveLoadManager Instance
    {
        get
        {
            if(mInstance == null)
                mInstance = FindObjectOfType<SaveLoadManager>();
            return mInstance;
        }
    }


    public int SceneToLoadIndex = -1;

    public Vector3 scenePosition;
    public Quaternion sceneRotation;
    public Vector3 sceneScale;

    public Vector3 SpawnPosition;

    private void Awake()
    {
        var go = FindObjectsOfType<SaveLoadManager>();
        if (go.Length > 1)
            DestroyImmediate(gameObject);
    }

    public void Start()
    {
        GameSceneManager.OnSceneChanged += GameSceneManager_OnSceneChanged;
    }

    private void GameSceneManager_OnSceneChanged()
    {
        SaveGame();
    }


    public void SaveGame()
    {
        var player = GameSceneManager.Instance.Player;
        var room = GameSceneManager.Instance.currentScene.transform;
        SaveSystem.SaveGame(player, room);
    }


    public void LoadSave()
    {
        var gameData = SaveSystem.LoadGame();
        SceneToLoadIndex = gameData.sceneIndex;

        scenePosition = new Vector3(gameData.roomPosition[0], gameData.roomPosition[1], gameData.roomPosition[2]);
        sceneRotation = new Quaternion(gameData.roomRotation[0], gameData.roomRotation[1], gameData.roomRotation[2], gameData.roomRotation[3]);
        sceneScale = new Vector3(gameData.roomScale[0], gameData.roomScale[1], gameData.roomScale[2]);

        SpawnPosition = new Vector3(gameData.playerPosition[0], gameData.playerPosition[1], gameData.playerPosition[2]);

    }

}
