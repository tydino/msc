using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_savedata : MonoBehaviour //copied from https://www.youtube.com/watch?v=pVXEUtMy_Hc for testing purposes. I have made modifications, but it is mostly the same.
{
    public string path;//edit #1, this allows for custom file paths outside of appdata which can allow for multiple save instances
    public test_Inventory inventory = new test_Inventory();//make sure this contains everything that is to be saved

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToJson();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadFromJson();
        }
    }

    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(inventory);
        string filePath = path + "/testing.json";//edit #2; just a continuation of edit #1 and is just a swap out for the custom path
        System.IO.File.WriteAllText(filePath, inventoryData);
    }

    public void LoadFromJson()
    {
        string filePath = path + "/testing.json";//edit #3; same as #2.
        string inventoryData = System.IO.File.ReadAllText(filePath);

        inventory = JsonUtility.FromJson<test_Inventory>(inventoryData);
    }
}

[System.Serializable]
public class test_Inventory
{
    public int GoldCoins;
    public bool IsFull;
    public List<test_items> items = new List<test_items>(); // this is something i was unable to do previously! :O
}

[System.Serializable]
public class test_items
{
    public string name;
    public string description;
}