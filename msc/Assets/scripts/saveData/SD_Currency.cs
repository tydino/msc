using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SD_Currency
{
    public sd_currency sdc;

    public void SaveToJson()
    {
        sdc.coins = Currency.coins;
        sdc.diamonds = Currency.diamonds;
        sdc.food = Currency.food;
        string data = JsonUtility.ToJson(sdc);
        string filePath = SaveData.current.path + "/currency.json";
        System.IO.File.WriteAllText(filePath, data);
    }

    public void LoadFromJson()
    {
        if (System.IO.File.Exists(SaveData.current.path + "/currency.json"))
        {
            string filePath = SaveData.current.path + "/currency.json";
            string data = System.IO.File.ReadAllText(filePath);

            sdc = JsonUtility.FromJson<sd_currency>(data);
            Currency.coins = sdc.coins;
            Currency.diamonds = sdc.diamonds;
            Currency.food = sdc.food;
        }
        else
        {
            Currency.coins = 500;
            Currency.diamonds = 50;
        }
    }
}

[System.Serializable]
public class sd_currency
{
    public int coins;
    public int diamonds;
    public int food;
}
