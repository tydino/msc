using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SD_ECTimer : MonoBehaviour
{
    public static SD_ECTimer current;
    public string path;
    public sd_ECTimer sdECT;

    public void SaveToJson()
    {
        sdECT.inProgress = EC_mainWidget.inProgress;
        sdECT.DateStart = EC_mainWidget.TimerStart.ToString();
        sdECT.DateEnd = EC_mainWidget.TimerEnd.ToString();
        sdECT.Status = EC_mainWidget.status.ToString();
        sdECT.creatureDone = EC_mainWidget.creatureDone;
        sdECT.creature1 = EC_mainWidget.creature1_Egg;
        sdECT.creature2 = EC_mainWidget.creature2_Egg;

        string data = JsonUtility.ToJson(sdECT);
        string filePath = path + "/EC_" + SaveData.current.islandString + ".json";
        System.IO.File.WriteAllText(filePath, data);
    }

    public void LoadFromJson()
    {
        if (System.IO.File.Exists(path + "/EC_" + SaveData.current.islandString + ".json"))
        {
            string filePath = path + "/EC_" + SaveData.current.islandString + ".json";
            string data = System.IO.File.ReadAllText(filePath);

            sdECT = JsonUtility.FromJson<sd_ECTimer>(data);

            EC_mainWidget.inProgress = sdECT.inProgress;
            EC_mainWidget.creatureDone = sdECT.creatureDone;
            EC_mainWidget.creature1_Egg = sdECT.creature1;
            EC_mainWidget.creature2_Egg = sdECT.creature2;

            EC_mainWidget.TimerStart = Convert.ToDateTime(sdECT.DateStart);
            EC_mainWidget.TimerEnd = Convert.ToDateTime(sdECT.DateEnd);

            if (sdECT.Status == "idle")
            {
                EC_mainWidget.status = EC_mainWidget.Status.idle;
            }
            if (sdECT.Status == "working")
            {
                EC_mainWidget.status = EC_mainWidget.Status.working;
            }
            if (sdECT.Status == "complete")
            {
                EC_mainWidget.status = EC_mainWidget.Status.complete;
            }
        }
    }
}

[System.Serializable]
public class sd_ECTimer
{
    public bool inProgress;
    public string DateStart;
    public string DateEnd;
    public string Status;
    public int creatureDone;
    public int creature1;
    public int creature2;
}
