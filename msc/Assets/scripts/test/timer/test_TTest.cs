using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//remember to input this one and system at start of scripts that shoot off from this.

public class test_TTest : MonoBehaviour//gotten from: https://www.youtube.com/watch?v=pTZfdXsNO1U
{
    public bool inProgress;
    public DateTime TimerStart;
    public DateTime TimerEnd;

    [Header("production time")]
    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    Coroutine lastTimer;
    Coroutine lastDisplay;

    [Header("UI")]
    [SerializeField] GameObject window;
    [SerializeField] Text startTimeText;
    [SerializeField] Text endTimeText;
    [SerializeField] GameObject timeLeftObj;
    [SerializeField] Text timeLeftText;
    [SerializeField] Slider timeLeftSlider;
    [SerializeField] Button skipButton;
    [SerializeField] Button startButton;//omit on future ones and instead have this one operation committed by an external voidusing StartTimer()

    #region Unity methods

    void Start()
    {
        startButton.onClick.AddListener(StartTimer);
        skipButton.onClick.AddListener(Skip);
        window.SetActive(false);
    }

    #endregion

    #region UI methods

    void InitializeWindow()
    {
        if (inProgress)
        {
            startTimeText.text = "start time: " + TimerStart;
            endTimeText.text = "end time: " + TimerEnd;

            timeLeftObj.SetActive(true);
            lastDisplay = StartCoroutine(DisplayTime());

            startButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            startTimeText.text = "start time: ";
            endTimeText.text = "end time: ";

            timeLeftObj.SetActive(false);
        }
    }

    IEnumerator DisplayTime()
    {
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimerEnd - start;
        double totalSecondsLeft = timeLeft.TotalSeconds;
        double totalSeconds = (TimerEnd - TimerStart).TotalSeconds;
        string text;

        while (window.activeSelf && timeLeftObj.activeSelf)
        {
            text = "";
            timeLeftSlider.value = 1 - Convert.ToSingle((TimerEnd - DateTime.Now).TotalSeconds / totalSeconds);
            skipButton.gameObject.SetActive(true);
            if (totalSecondsLeft > 1)
            {
                if (timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h";
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if (timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m";
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Seconds + "s";
                }
                else
                {
                    text += Mathf.FloorToInt((float)totalSecondsLeft) + "s";
                }

                timeLeftText.text = text;

                totalSecondsLeft -= Time.deltaTime;
                yield return null;

            }
            else
            {
                timeLeftText.text = "Finished";
                skipButton.gameObject.SetActive(false);
                timeLeftSlider.value = 1;
                inProgress = false;
                break;
            }
        }

        yield return null;
    }
    public void OpenWindow()
    {
        window.SetActive(true);
        InitializeWindow();
    }

    public void CloseWindow()
    {
        window.SetActive(false);
    }
    #endregion

    #region Timed event

    void StartTimer()
    {
        TimerStart = DateTime.Now;
        TimeSpan time = new TimeSpan(Days, Hours, Minutes, Seconds);
        TimerEnd = TimerStart.Add(time);
        inProgress = true;

        lastTimer = StartCoroutine(Timer());

        InitializeWindow();
    }

    IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secondsToFinished = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));

        inProgress = false;
        Debug.Log("complete!");
    }


    void Skip()
    {
        TimerEnd = DateTime.Now;
        inProgress = false;
        StopCoroutine(lastTimer);

        timeLeftText.text = "Finished";
        timeLeftSlider.value = 1;

        StopCoroutine(lastDisplay);
        skipButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    #endregion
}
