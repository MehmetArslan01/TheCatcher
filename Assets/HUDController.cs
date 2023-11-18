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


    // Start is called before the first frame update
    void Start()
    {
        this.matchTimeInSec = (this.matchTimeInMin * 60);
    }

    // Update is called once per frame
    void Update()
    {
        this.matchTimeInSec -= Time.deltaTime;
        var ts = TimeSpan.FromSeconds(this.matchTimeInSec);
        this.time.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        if (this.matchTimeInSec <= 0.0f)
        {
            timerFinished();
        }
    }

    void timerFinished()
    {
        //TODO: do something after timer finished
    }
}
