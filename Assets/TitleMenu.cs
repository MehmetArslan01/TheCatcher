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
        this.ShowNewGroup(this.manualGroup, this.startGroup);
    }

    public void ShowSettings()
    {
        this.ShowNewGroup(this.settingsGroup, this.startGroup);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowStart(CanvasGroup currentGroup)
    {
        this.ShowNewGroup(this.startGroup, currentGroup);
    }

    private void ShowNewGroup(CanvasGroup newGroup, CanvasGroup currentGroup)
    {
        this.DisableGroup(currentGroup);

        newGroup.alpha = 1.0f;
        newGroup.interactable = true;
        newGroup.blocksRaycasts = true;
    }

    private void DisableGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
