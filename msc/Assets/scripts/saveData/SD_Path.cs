using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SD_Path
{
    public sd_Path path;

    public void PathVoid(string PathStr)
    {
        path.path = PathStr;
        SaveToJson();
    }

    public void SaveToJson()
    {
        string data = JsonUtility.ToJson(path);
        string filePath = Application.persistentDataPath + "/Save.json";
        System.IO.File.WriteAllText(filePath, data);
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/Save.json";
        string data = System.IO.File.ReadAllText(filePath);

        path = JsonUtility.FromJson<sd_Path>(data);
    }
}

[System.Serializable]
public class sd_Path
{
    public string path;
}