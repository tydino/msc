using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public SD_Currency sdc;
    public SD_Island sdi;
    public int coin_editable;
    public int diamond_editable;
    public int food_editable;

    public void save()
    {
        if (coin_editable != 0 || diamond_editable != 0 || food_editable != 0)
        {
            if (coin_editable != 0) { Currency.coins = coin_editable; }
            if (diamond_editable != 0) { Currency.diamonds = diamond_editable; }
            if (food_editable != 0) { Currency.food = food_editable; }
        }
        sdc.SaveToJson();
        sdi.SaveToJson();
    }
    public void load()
    {
        sdc.LoadFromJson();
        sdi.LoadFromJson();
    }

    void Awake()
    {
        load();
    }
}
