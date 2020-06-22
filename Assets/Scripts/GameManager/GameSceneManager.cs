using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager mInstance;
    public static GameSceneManager Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = FindObjectOfType<GameSceneManager>();
            return mInstance;
        }
    }

    public delegate void SceneChanged();
    public static event SceneChanged OnSceneChanged;

    public GameObject currentScene;
    public GameObject previousScene;

    public Transform Player;
    public Transform viewsParent;

    private SaveLoadManager saveLoadManager;

    public void Start()
    {
        saveLoadManager = SaveLoadManager.Instance;
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        if(viewsParent == null)
            viewsParent = GameObject.Find("Views").transform;
        TryLoadScene();
        UpdateCameras();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    public void ReloadScene()
    {
        saveLoadManager.LoadSave();

        TryLoadScene();

        Player.GetComponent<PlayerMovementController>().PlayerRespawn = false;
    }

    private void TryLoadScene()
    {
        if (saveLoadManager.SceneToLoadIndex > -1)
        {
            var scene = viewsParent.GetChild(saveLoadManager.SceneToLoadIndex);
            if (scene != null)
            {

                scene.GetComponent<ResetView>().ResetTransform(saveLoadManager.scenePosition, saveLoadManager.sceneRotation, saveLoadManager.sceneScale);
                Player.GetComponent<PlayerMovementController>().ResetPlayerValues();
                Player.transform.position = saveLoadManager.SpawnPosition;
                ChangeToScene(scene.gameObject);
                
            }
        }
        else
        {
            var scene = viewsParent.GetChild(0).gameObject;
            ChangeToScene(scene);
        }
    }

    public void ChangeToScene(GameObject newScene)
    {

        previousScene = currentScene;
        currentScene = newScene;

        if (previousScene != null)
        {
            previousScene.SetActive(false);
        }
        if (currentScene != null)
        {
            OnSceneChanged?.Invoke();
            currentScene.SetActive(true);
            UpdateCameras();
        }
    }

    public void UpdateCameras()
    {
        if (currentScene == null)
            return;

        var renderer = currentScene.GetComponent<SpriteRenderer>();
        var mainCam = Camera.main;
        var newPos = new Vector3(renderer.bounds.center.x, renderer.bounds.center.y, Camera.main.transform.position.z);

        var spriteWidth = renderer.bounds.size.x;
        var spriteHeight = renderer.bounds.size.y;

        mainCam.GetComponent<CameraMovementController>().TargetPos = newPos;
        mainCam.GetComponent<CameraMovementController>().TargetScale = (spriteWidth > spriteHeight ? spriteWidth : spriteHeight)/2;

    }


}
