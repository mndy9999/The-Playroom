using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager mInstance;
    public static SoundManager Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = FindObjectOfType<SoundManager>();
            return mInstance;
        }
    }

    public int Volume;
}
