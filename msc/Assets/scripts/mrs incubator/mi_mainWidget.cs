using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mi_mainWidget : MonoBehaviour
{
    public static mi_mainWidget current;

    //save data things
    public static bool inProgress;
    public static Status status;
    public static int creatureDone;
    public static DateTime TimerStart;
    public static DateTime TimerEnd;

    List<creatureData> creatureDatas = new List<creatureData>();
    List<GameObject> creaturePrefabs = new List<GameObject>();

    Text TimeLeft;
    Slider TimeLeftSlider;
    GameObject TimeLeftObj;


    #region enum
    public enum Status { idle, working, complete }
    #endregion

    #region activation
    void OnMouseDown()
    {
        if (status == Status.complete)
        {
            GridBuildingSystem.current.InitializeWithBuilding(creaturePrefabs[creatureDone-1]);
            interactionHandler.current.OpenUI(true);
            status = Status.idle;
            creatureDone = 0;
            inProgress = false;
            SaveData.current.save();
        }
    }
    #endregion


    #region unity voids
    void Awake()
    {
        current = this;
    }

    void Start()
    {
        TimeLeft = interactionHandler.current.TimeLeft1;
        TimeLeftSlider = interactionHandler.current.TimeLeftSlider1;
        TimeLeftObj = interactionHandler.current.TimeLeftObj1;
        foreach (creatureData cd in creatureHandler.current.creatureObjects)
        {
            creaturePrefabs.Add(cd.PrefabObj);
            creatureDatas.Add(cd);
        }
        interactionHandler.current.TimeLeftObj1.SetActive(false);
        SetUpTimer();
    }
    void Update()
    {
        SetUpTimer();
    }
    #endregion

    #region timer

    //figure out skip button eventually

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
        status = Status.working;
        creatureData cd = creatureDatas[i];
        //initialize after data
        creatureDone = cd.creatureInIslandID;
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
    }

    #region iEnumerators
    IEnumerator DisplayTime()
    {
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimerEnd - start;
        double totalSecondsLeft = timeLeft.TotalSeconds;
        double totalSeconds = (TimerEnd - TimerStart).TotalSeconds;
        string text;
        while (status == Status.working)
        {
            text = "";
            interactionHandler.current.TimeLeftSlider1.value = 1 - Convert.ToSingle((TimerEnd - DateTime.Now).TotalSeconds / totalSeconds);
            interactionHandler.current.TimeLeftObj1.SetActive(true);
            //skipButton.gameObject.SetActive(true);

            if (totalSecondsLeft > 1)
            {
                if (timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h";
                    interactionHandler.current.TimeLeft1.text = text;
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if (timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m";
                    interactionHandler.current.TimeLeft1.text = text;
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Seconds + "s";
                    interactionHandler.current.TimeLeft1.text = text;
                }
                else
                {
                    text += Mathf.FloorToInt((float)totalSecondsLeft) + "s";
                    interactionHandler.current.TimeLeft1.text = text;
                }

                totalSecondsLeft -= Time.deltaTime;
                yield return null;

            }
            else
            {
                interactionHandler.current.TimeLeft1.text = "Finished";
                //skipButton.gameObject.SetActive(false);
                interactionHandler.current.TimeLeftSlider1.value = 1;
                interactionHandler.current.TimeLeftObj1.SetActive(false);
                status = Status.complete;
                inProgress = false;
            }
        }

        yield return null;
    }

    IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secondsToFinished = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));
        Debug.Log("complete!");
    }
    #endregion
    #endregion
}
