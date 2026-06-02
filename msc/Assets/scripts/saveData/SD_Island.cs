using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Island : MonoBehaviour
{
    public static SD_Island current;
    public sd_Island island;

    public void Awake()
    {
        current = this;
    }

    public void SaveToJson()
    {
        creatureHandler.current.compileCreatures();
        string filePath = SaveData.current.path + "/islands";
        //checks file directory/folder
        if (!System.IO.Directory.Exists(filePath))
        {
            System.IO.Directory.CreateDirectory(filePath);
        }
        //goes through objects
        if (!System.IO.File.Exists(filePath + "/" + SaveData.current.islandString + ".json"))
        {
            objectHandler.current.compileObjects(true);
        }
        else 
        {
            objectHandler.current.compileObjects(false);
        }
        string islandData = JsonUtility.ToJson(island);
        System.IO.File.WriteAllText(filePath + "/" + SaveData.current.islandString + ".json", islandData);
    }

    public void LoadFromJson()
    {
        if (System.IO.File.Exists(SaveData.current.path + "/islands/" + SaveData.current.islandString + ".json"))
        {
            ///     CREATURES   ///
            creatureHandler.current.creatureInformation.Clear();
            GameObject[] creaturesEXE = GameObject.FindGameObjectsWithTag("creature");
            foreach (GameObject creatureToDestroy in creaturesEXE)
            {
               Destroy(creatureToDestroy);
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
            ///     OBJECTS     ///
            objectHandler.current.objectInformation.Clear();
            GameObject[] objectsEXE = GameObject.FindGameObjectsWithTag("object");
            foreach(GameObject objectToDestroy in objectsEXE)
            {
                Destroy(objectToDestroy);
            }
            foreach (sd_ObjectHandler sdoh in island.OHList)
            {
                Vector3 pos = new Vector3(sdoh.XPos, sdoh.YPos, 1);
                Vector3 scale = new Vector3(sdoh.XScl, 1, 1);
                GameObject od = objectHandler.current.objectObjects[sdoh.ObjectID].PrefabObj;
                GameObject clone = Instantiate(od);
                clone.transform.position = pos;
                clone.transform.localScale = scale;
                clone.GetComponent<Building>().Placed = true;
                clone.GetComponent<objectControler>().DecompileData(sdoh.Data);
                objectHandler.current.objectInformation.Add(sdoh);
            }
            ///EC_mainWidget.current.setUpEC(); FIX THIS
        }
        else
        {
            objectHandler.current.compileObjects(true);
            foreach (sd_ObjectHandler sdoh in island.OHList)
            {
                Vector3 pos = new Vector3(sdoh.XPos, sdoh.YPos, 1);
                Vector3 scale = new Vector3(sdoh.XScl, 1, 1);
                GameObject od = objectHandler.current.objectObjects[sdoh.ObjectID].PrefabObj;
                GameObject clone = Instantiate(od);
                clone.transform.position = pos;
                clone.transform.localScale = scale;
                clone.GetComponent<Building>().Placed = true;
                objectHandler.current.objectInformation.Add(sdoh);
            }
        }
    }
}

[System.Serializable]
public class sd_Island
{
    public List<sd_CreatureHandler> CHList = new List<sd_CreatureHandler>();
    public List<sd_ObjectHandler> OHList = new List<sd_ObjectHandler>();
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

[System.Serializable]
public class sd_ObjectHandler
{
    public float XPos;
    public float YPos;
    public float XScl;
    public int ObjectID;
    public string Data;
}