using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : Interactable
{
    public GameObject gameOverScreen;

    private void Start()
    {
        IsInstant = true;
    }

    public override void Interact(Transform obj)
    {
        AudioManager.Instance.PlayOutroTheme();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
