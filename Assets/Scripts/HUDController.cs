using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI time;
    public float matchTimeInMin;
    private float matchTimeInSec;

    private static int p1LeoHP;
    private static int p2LeoHP;
    public TextMeshProUGUI p1Health;
    public TextMeshProUGUI p2Health;

    public static bool isGameOver = false;
    private bool isGameOverScreenVisible = false;

    public CanvasGroup hudGroup;
    public CanvasGroup gameOverGroup;

    // Start is called before the first frame update
    void Start()
    {
        this.matchTimeInSec = (this.matchTimeInMin * 60);

        p1LeoHP = 1;
        p2LeoHP = 1;

        this.p1Health.text = p1LeoHP.ToString();
        this.p2Health.text = p2LeoHP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.matchTimeInSec <= 0.0f || IsPlayerDead() && !isGameOverScreenVisible)
        {
            TimerFinished();
            this.isGameOverScreenVisible = true;
        }
        else
        {
            this.matchTimeInSec -= Time.deltaTime;
            var ts = TimeSpan.FromSeconds(this.matchTimeInSec);
            this.time.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }

        if (p1LeoHP != (PlayerMovement.numberOfLeos + 1))
        {
            p1LeoHP = (PlayerMovement.numberOfLeos + 1);
            p1Health.text = p1LeoHP.ToString();
        }

        if (p2LeoHP != (PlayerMovementP2.numberOfLeos + 1))
        {
            p2LeoHP = (PlayerMovementP2.numberOfLeos + 1);
            p2Health.text = p2LeoHP.ToString();
        }

        if (isGameOver == true)
        {
            GameOverController gameOverController = this.gameOverGroup.GetComponent<GameOverController>();
            gameOverController.SetScoreAndWinnner(p1LeoHP, p2LeoHP);
        }
    }

    private void TimerFinished()
    {
        this.StartCoroutine(this.FadeInGameOverRoutine());
        gameOverGroup.interactable = true;
        gameOverGroup.blocksRaycasts = true;
    }

    private bool IsPlayerDead()
    {
        if (p1LeoHP < 1 || p2LeoHP < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator FadeInGameOverRoutine()
    {
        const float fadeInTime = 0.25f;

        float timer = 0.0f;
        while (timer < fadeInTime)
        {
            float percent = timer / fadeInTime;

            this.gameOverGroup.alpha = 0.0f + percent;
            this.hudGroup.alpha = 1.0f - percent;

            yield return null;

            timer += Time.deltaTime;
        }
        this.gameOverGroup.alpha = 1.0f;
        isGameOver = true;
        this.hudGroup.alpha = 0.0f;
    }

    public float GetTimePercent()
    {
        return matchTimeInSec / (matchTimeInMin * 60);
    }

}
