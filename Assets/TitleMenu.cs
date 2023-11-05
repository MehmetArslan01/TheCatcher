using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public CanvasGroup startGroup;
    public CanvasGroup manualGroup;
    public CanvasGroup settingsGroup;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Start Screen clicked mf");
    }

    public void ShowManual()
    {
        this.startGroup.alpha = 0.0f;
        this.manualGroup.alpha = 1.0f;
    }

    public void ShowSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
