using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsManager : MonoBehaviour
{

    private static CinematicsManager mInstance;
    public static CinematicsManager Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = FindObjectOfType<CinematicsManager>();
            return mInstance;
        }
    }


    Queue<Cinematic> clips;
    private bool mCinematicsStarted;
    public bool CinematicsStarted
    {
        get
        {
            return mCinematicsStarted;
        }
        set
        {
            if(mCinematicsStarted != value)
            {
                mCinematicsStarted = value;
            }
        }
    }

    private PlayerMovementController playerController;
    public ButterflyController butterflyController;

    public bool CanContinue;

    public void CreateButter(GameObject butter)
    {
        var go = Instantiate(butter);
        butterflyController = go.GetComponent<ButterflyController>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        clips = new Queue<Cinematic>();
        playerController = GameSceneManager.Instance.Player.GetComponent<PlayerMovementController>();
    }

    public void StartCinematics(Cinematic[] cinematic)
    {
        CinematicsStarted = true;

        clips.Clear();

        foreach (var c in cinematic)
        {
            clips.Enqueue(c);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (clips.Count == 0)
        {
            EndCinematics();
            return;
        }
        var clip = clips.Dequeue();
        StartCoroutine(clip.Play());
    }

    private void EndCinematics()
    {
        CinematicsStarted = false;
    }

    private void Update()
    {
       // if (CinematicsStarted && Input.GetKeyDown(KeyCode.Space) && CanContinue)
      //  {
           // DisplayNextSentence();
       // }
    }
     

}


