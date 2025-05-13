using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Currency : MonoBehaviour
{
    public string path;
    public sd_currency sdc;

    public void SaveToJson()
    {
        sdc.coins = Currency.coins;
        sdc.diamonds = Currency.diamonds;
        sdc.food = Currency.food;
        string data = JsonUtility.ToJson(sdc);
        string filePath = path + "/currency.json";
        System.IO.File.WriteAllText(filePath, data);
    }

    public void LoadFromJson()
    {
        string filePath = path + "/currency.json";
        string data = System.IO.File.ReadAllText(filePath);

        sdc = JsonUtility.FromJson<sd_currency>(data);
        Currency.coins = sdc.coins;
        Currency.diamonds = sdc.diamonds;
        Currency.food = sdc.food;
    }
}

[System.Serializable]
public class sd_currency
{
    public int coins;
    public int diamonds;
    public int food;
}
