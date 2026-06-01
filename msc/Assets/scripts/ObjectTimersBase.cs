using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectTimersBase : MonoBehaviour
{

    public bool inProgress;
    public Status status;
    public enum Status { idle, working, complete }

    public DateTime TimerStart;
    public DateTime TimerEnd;

    Coroutine lastTimer;
    Coroutine lastDisplay;

    public void SetUpTimer()
    {

        lastDisplay = StartCoroutine(DisplayTime());
    }

    public void StartTimer(int i)
    {
        inProgress = true;
        //initialize before data
        creatureData cd = creatureHandler.current.creatureObjects[i];
        //initialize after data
        TimerStart = DateTime.Now;
        int Days = cd.timeInDays;
        int Hours = cd.timeInHours;
        int Minutes = cd.timeInMinutes;
        int Seconds = cd.timeInSeconds;
        TimeSpan time = new TimeSpan(Days, Hours, Minutes, Seconds);
        TimerEnd = TimerStart.Add(time);

        SaveData.current.save();
        SetUpTimer();
        lastTimer = StartCoroutine(Timer());
        //status = Status.working;
    }

    #region iEnumerators
    public IEnumerator DisplayTime()
    {
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimerEnd - start;
        double totalSecondsLeft = timeLeft.TotalSeconds;
        double totalSeconds = (TimerEnd - TimerStart).TotalSeconds;
        string text;
        while (status == Status.working)
        {
            text = "";
            interactionHandler.current.TimeLeftSlider.value = 1 - Convert.ToSingle((TimerEnd - DateTime.Now).TotalSeconds / totalSeconds);
            interactionHandler.current.TimeLeftObj.SetActive(true);
            //skipButton.gameObject.SetActive(true);

            if (totalSecondsLeft > 1)
            {
                if (timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h";
                    interactionHandler.current.TimeLeft.text = text;
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if (timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m";
                    interactionHandler.current.TimeLeft.text = text;
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Seconds + "s";
                    interactionHandler.current.TimeLeft.text = text;
                }
                else
                {
                    text += Mathf.FloorToInt((float)totalSecondsLeft) + "s";
                    interactionHandler.current.TimeLeft.text = text;
                }

                totalSecondsLeft -= Time.deltaTime;
                yield return null;

            }
            else
            {
                interactionHandler.current.TimeLeft.text = "Finished";
                //skipButton.gameObject.SetActive(false);
                interactionHandler.current.TimeLeftSlider.value = 1;
                interactionHandler.current.TimeLeftObj.SetActive(false);
                status = Status.complete;
                inProgress = false;
            }
        }

        yield return null;
    }

    public IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secondsToFinished = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));
        Debug.Log("complete!");
    }
    #endregion
}
