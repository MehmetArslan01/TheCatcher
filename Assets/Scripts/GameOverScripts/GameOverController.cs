using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI p1Score;
    public TextMeshProUGUI p2Score;
    public TextMeshProUGUI winner;

    public void SetScoreAndWinnner(int p1LeoCount, int p2LeoCount)
    {
        this.p1Score.text = "Player 1: " + p1LeoCount;
        this.p2Score.text = "Player 2: " + p2LeoCount;

        if (p1LeoCount > p2LeoCount)
        {
            winner.text = "Player 1";
        }
        else if (p2LeoCount > p1LeoCount)
        {
            winner.text = "Player 2";
        }
        else
        {
            winner.text = "Draw";
        }
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
