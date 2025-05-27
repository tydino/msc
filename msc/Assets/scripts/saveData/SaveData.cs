using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData current;
    public SD_Currency sdc;
    public SD_Island sdi;
    public SD_Path sdp;
    public SD_ECTimer sdect;
    public SD_MITimer sdmit;
    public int coin_editable;
    public int diamond_editable;
    public int food_editable;
    public string path; //my path: C:\Users\tydin\OneDrive\Documents\mscTesting

    public void save()
    {
        sdp.SaveToJson();
        if (coin_editable != 0 || diamond_editable != 0 || food_editable != 0)
        {
            if (coin_editable != 0) { Currency.coins = coin_editable; }
            if (diamond_editable != 0) { Currency.diamonds = diamond_editable; }
            if (food_editable != 0) { Currency.food = food_editable; }
        }
        sdc.path = path;
        sdi.path = path;
        sdect.path = path;
        sdmit.path = path;
        sdc.SaveToJson();
        sdi.SaveToJson();
        sdect.SaveToJson();
        sdmit.SaveToJson();
    }
    public void load()
    {
        sdp.LoadFromJson();
        path = sdp.path.path;
        sdc.path = path;
        sdi.path = path;
        sdect.path = path;
        sdmit.path = path;
        sdc.LoadFromJson();
        sdi.LoadFromJson();
        sdect.LoadFromJson();
        sdmit.LoadFromJson();
    }

    void Start()
    {
        current = this;
        load();
        save();
    }
    public void pathFinding(string Path)
    {
        sdp.path.path = Path;
        path = sdp.path.path;
    }
}
