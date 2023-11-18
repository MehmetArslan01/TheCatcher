using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI time;
    public float matchTimeInMin;
    public CanvasGroup gameOverGroup;

    private float matchTimeInSec;


    // Start is called before the first frame update
    void Start()
    {
        this.matchTimeInSec = (this.matchTimeInMin * 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.matchTimeInSec <= 0.0f)
        {
            TimerFinished();
        }
        else
        {
            this.matchTimeInSec -= Time.deltaTime;
            var ts = TimeSpan.FromSeconds(this.matchTimeInSec);
            this.time.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }
    }

    private void TimerFinished()
    {
        this.StartCoroutine(this.FadeInGameOverRoutine());
        gameOverGroup.interactable = true;
        gameOverGroup.blocksRaycasts = true;
    }


    private IEnumerator FadeInGameOverRoutine()
    {
        const float fadeInTime = 0.25f;

        float timer = 0.0f;
        while (timer < fadeInTime)
        {
            float percent = timer / fadeInTime;

            this.gameOverGroup.alpha = 0.0f + percent;

            yield return null;

            timer += Time.deltaTime;
        }
        this.gameOverGroup.alpha = 1.0f;
    }
}
