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
    }

    public void ShowManual()
    {
        this.DisableGroup(startGroup);

        this.manualGroup.alpha = 1.0f;
        this.manualGroup.interactable = true;
    }

    public void ShowSettings()
    {
        this.DisableGroup(startGroup);

        this.settingsGroup.alpha = 1.0f;
        this.settingsGroup.interactable = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowStart(CanvasGroup currentGroup)
    {
        this.DisableGroup(currentGroup);
        this.startGroup.alpha = 1.0f;
        this.startGroup.interactable = true;
    }

    private void DisableGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
    }

}
