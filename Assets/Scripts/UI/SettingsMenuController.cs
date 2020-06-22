using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public Slider slider;
    private void OnEnable()
    {
        slider.value = AudioManager.Instance.Volume;
    }

    public void SetVolume(float volume)
    {
        AudioManager.Instance.UpdateSoundsVolume(volume);
    }

}
