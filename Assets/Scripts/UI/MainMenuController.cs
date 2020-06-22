using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;

    public Button loadButton;

    private SaveLoadManager loadManager;

    private void Awake()
    {
        var saveExists = File.Exists(SaveSystem.FilePath);
        loadButton.interactable = saveExists;
        if(!saveExists)
             loadButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.grey;
        loadManager = SaveLoadManager.Instance;
    }

    public void Play()
    {
        loadManager.SceneToLoadIndex = -1;
        AudioManager.Instance.PlayMainTheme();
        SceneManager.LoadScene(1);
    }

    public void OpenCloseSettings()
    {
        mainPanel.SetActive(!mainPanel.activeSelf);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Load()
    {
        SaveLoadManager.Instance.LoadSave();
        AudioManager.Instance.PlayMainTheme();
        SceneManager.LoadScene(1);
    }

}
