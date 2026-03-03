using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SD_MITimer
{
    public static SD_MITimer current;
    public sd_MITimer sdMIT;

    public void SaveToJson()
    {
        sdMIT.inProgress = mi_mainWidget.inProgress;
        sdMIT.DateStart = mi_mainWidget.TimerStart.ToString();
        sdMIT.DateEnd = mi_mainWidget.TimerEnd.ToString();
        sdMIT.Status = mi_mainWidget.status.ToString();
        sdMIT.creatureDone = mi_mainWidget.creatureDone;

        string data = JsonUtility.ToJson(sdMIT);
        string filePath = SaveData.current.path + "/islands/MI";
        if (!System.IO.Directory.Exists(filePath))
        {
            System.IO.Directory.CreateDirectory(filePath);
        }
        System.IO.File.WriteAllText(filePath + "/" + SaveData.current.islandString + ".json", data);
    }

    public void LoadFromJson()
    {
        if (System.IO.File.Exists(SaveData.current.path + "/islands/MI/" + SaveData.current.islandString + ".json"))
        {
            string filePath = SaveData.current.path + "/islands/MI/" + SaveData.current.islandString + ".json";
            string data = System.IO.File.ReadAllText(filePath);

            sdMIT = JsonUtility.FromJson<sd_MITimer>(data);

            mi_mainWidget.inProgress = sdMIT.inProgress;
            mi_mainWidget.creatureDone = sdMIT.creatureDone;

            mi_mainWidget.TimerStart = Convert.ToDateTime(sdMIT.DateStart);
            mi_mainWidget.TimerEnd = Convert.ToDateTime(sdMIT.DateEnd);

            if (sdMIT.Status == "idle")
            {
                mi_mainWidget.status = mi_mainWidget.Status.idle;
            }
            if (sdMIT.Status == "working")
            {
                mi_mainWidget.status = mi_mainWidget.Status.working;
            }
            if (sdMIT.Status == "complete")
            {
                mi_mainWidget.status = mi_mainWidget.Status.complete;
            }
        }
        else
        {
            Debug.Log("No data to load");
        }
    }
}

[System.Serializable]
public class sd_MITimer
{
    public bool inProgress;
    public string DateStart;
    public string DateEnd;
    public string Status;
    public int creatureDone;
}
