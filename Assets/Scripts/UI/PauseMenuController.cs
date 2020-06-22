using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseResume();
    }

    public void PauseResume()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void OpenCloseSettings()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlayMenuTheme();
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }



}
