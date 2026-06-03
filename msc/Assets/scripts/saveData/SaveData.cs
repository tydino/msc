using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public string islandString;
    public bool Saves = true;
    public static SaveData current;
    public SD_Currency sdc;
    public SD_Island sdi;
    public SD_Path sdp;
    public string path; //my path: C:\Users\tydin\OneDrive\Documents\mscTesting

    public void save()
    {
        if (Saves)
        {
            sdp.SaveToJson();
            sdc.SaveToJson();
            sdi.SaveToJson();
        }
    }
    public void load()
    {
        if (Saves)
        {
            sdp.LoadFromJson();
            path = sdp.path.path;
            sdc.LoadFromJson();
            sdi.LoadFromJson();
        }
    }

    void Start()
    {
        load();
        save();
    }
    public void pathFinding(string Path)
    {
        sdp.path.path = Path;
        path = sdp.path.path;
    }

    private void Awake()
    {
        current = this;
    }
}
