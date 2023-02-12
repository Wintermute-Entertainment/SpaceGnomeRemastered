using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; //Timer text object.
    public float startTime;     //Time to start countdown timer at.
    public float timerTime;     //Timer current time.

    public static bool timerFinished;
    public bool timerPaused;

    public float t;         //Used to calculate time left on timer (negative value).

    private static float remainingMinutes;  //Unused currently, maybe later for UI.
    private static float remainingSeconds;
    private void Awake()
    {
        timerPaused = false;
        timerFinished = false;
    }
    private void Start()
    {
        startTime = Time.time + timerTime; //Time in seconds to count down from.
    }
    void Update()
    {

        if (timerFinished)      //True at game over condition when timer runs out.
            return;

        t = Time.time - startTime; //Time variable.

        string minutes = ((int)t / 60).ToString();  //Minutes string.
        string seconds = (-t % 60).ToString(format: "f1"); //Seconds string to one decimal place.

        remainingMinutes = t + startTime + Time.time / 60;
        remainingSeconds = t + startTime + Time.time;

        timerText.text = minutes + ":" + seconds; //Text object output.

        if (t >= 0)                 //Timer stop and Game Over condition.
        {
            TimerStop();
            Toolbox.instance.m_playerManager.GameOver();  //GameOver() call.
        }
        
    }
    public void TimerStop()
    {
        timerFinished = true;
    }
    public void TimerPause()
    {
        timerPaused = true;
    }
}
