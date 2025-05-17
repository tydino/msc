using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_TTestData : MonoBehaviour
{
    public string path;
    public string placeID;
    public List<test_TTest> ttt;//must match timedata's time data amount
    public sd_TTest timeData;

    /*public void SaveToJson()//figure out later
    {
        for (int i; i < test_TTest.Count; i++) 
        {
            timeData.timeStart[i].Add(ttt[i].TimerStart.ToString());
        }
        string TimeData = JsonUtility.ToJson(timeData);
        string filePath = path + "/" + placeID + "Timers.json";//edit #2; just a continuation of edit #1 and is just a swap out for the custom path
        System.IO.File.WriteAllText(filePath, TimeData);
    }

    public void LoadFromJson()
    {
        string filePath = path + "/" + placeID + "Timers.json";//edit #3; same as #2.
        string TimeData = System.IO.File.ReadAllText(filePath);

        timeData = JsonUtility.FromJson<sd_TTest>(TimeData);
        for(int i; i< timeData.timeStart.Count;i++)
        {
            ttt[i].TimerStart = DateTime.ParseExtract(timeData.timeStart[i], "dd/mm/ss", null);
        }
    }*/
}

[System.Serializable]
public class sd_TTest
{
    public List<string> timeStart;
    public List<string> timeEnd;
    public List<bool> inProgress;
}