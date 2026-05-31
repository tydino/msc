using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SD_ECTimer : MonoBehaviour
{/*
    public static SD_ECTimer current;
    public sd_ECTimer sdECT;

    public void SaveToJson()
    {
        sdECT.inProgress = EC_mainWidget.current.inProgress;
        sdECT.DateStart = EC_mainWidget.current.TimerStart.ToString();
        sdECT.DateEnd = EC_mainWidget.current.TimerEnd.ToString();
        sdECT.Status = EC_mainWidget.current.status.ToString();
        sdECT.creatureDone = EC_mainWidget.creatureDone;
        sdECT.creature1 = EC_mainWidget.creature1_Egg;
        sdECT.creature2 = EC_mainWidget.creature2_Egg;

        string data = JsonUtility.ToJson(sdECT);
        string filePath = SaveData.current.path + "/islands/EC";
        if (!System.IO.Directory.Exists(filePath))
        {
            System.IO.Directory.CreateDirectory(filePath);
        }
        System.IO.File.WriteAllText(filePath + "/" + SaveData.current.islandString + ".json", data);
    }

    public void LoadFromJson()
    {
        if (System.IO.File.Exists(SaveData.current.path + "/islands/EC/" + SaveData.current.islandString + ".json"))
        {
            string filePath = SaveData.current.path + "/islands/EC/" + SaveData.current.islandString + ".json";
            string data = System.IO.File.ReadAllText(filePath);

            sdECT = JsonUtility.FromJson<sd_ECTimer>(data);

            EC_mainWidget.current.inProgress = sdECT.inProgress;
            EC_mainWidget.creatureDone = sdECT.creatureDone;
            EC_mainWidget.creature1_Egg = sdECT.creature1;
            EC_mainWidget.creature2_Egg = sdECT.creature2;

            EC_mainWidget.current.TimerStart = Convert.ToDateTime(sdECT.DateStart);
            EC_mainWidget.current.TimerEnd = Convert.ToDateTime(sdECT.DateEnd);

            if (sdECT.Status == "idle")
            {
                EC_mainWidget.current.status = EC_mainWidget.Status.idle;
            }
            if (sdECT.Status == "working")
            {
                EC_mainWidget.current.status = EC_mainWidget.Status.working;
            }
            if (sdECT.Status == "complete")
            {
                EC_mainWidget.current.status = EC_mainWidget.Status.complete;
            }
        }
        else
        {
            Debug.Log("No data to load");
        }
    }*/
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
