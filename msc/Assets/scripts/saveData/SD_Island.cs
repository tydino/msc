using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Island : MonoBehaviour
{
    public sd_Island island;

    public void SaveToJson()
    {
        creatureHandler.current.compileCreatures();
        string islandData = JsonUtility.ToJson(island);
        string filePath = SaveData.current.path + "/islands";
        if (!System.IO.Directory.Exists(filePath))
        {
            System.IO.Directory.CreateDirectory(filePath);
        }
        System.IO.File.WriteAllText(filePath + "/" + SaveData.current.islandString + ".json", islandData);
    }

    public void LoadFromJson()
    {
        if (System.IO.File.Exists(SaveData.current.path + "/islands/" + SaveData.current.islandString + ".json"))
        {
            creatureHandler.current.creatureInformation.Clear();
            GameObject[] creaturesEXE = GameObject.FindGameObjectsWithTag("creature");
            foreach (GameObject creatureToDestroy in creaturesEXE)
            {
                GameObject.Destroy(creatureToDestroy);
            }
            string filePath = SaveData.current.path + "/islands/" + SaveData.current.islandString + ".json";
            string islandData = System.IO.File.ReadAllText(filePath);

            island = JsonUtility.FromJson<sd_Island>(islandData);
            foreach (sd_CreatureHandler sdch in island.CHList)
            {
                Vector3 pos = new Vector3(sdch.XPos, sdch.YPos, 1);
                Vector3 scale = new Vector3(sdch.XScl, 1, 1);
                GameObject cd = creatureHandler.current.creatureObjects[sdch.CreatureID - 1].PrefabObj;
                GameObject clone = Instantiate(cd);
                clone.GetComponent<creatureControler>().sleep = sdch.asleep;
                clone.transform.position = pos;
                clone.transform.localScale = scale;
                clone.GetComponent<Building>().Placed = true;
                creatureHandler.current.creatureInformation.Add(sdch);
            }
            EC_mainWidget.current.setUpEC();
        }
        else
        {
            Debug.Log("No data to load");
        }
    }
}

[System.Serializable]
public class sd_Island
{
    public List<sd_CreatureHandler> CHList = new List<sd_CreatureHandler>();
}

[System.Serializable]
public class sd_CreatureHandler
{
    public float XPos;
    public float YPos;
    public float XScl;
    public int CreatureID;
    public bool asleep;
}
