using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectHandler : MonoBehaviour
{
    public static objectHandler current;
    [Header("add 1 to list index for available objects")]
    public List<objectData> objectObjects;
    [Header("Every starting object must be set here")]
    public List<sd_ObjectHandler> objectStart;
    [Header("this is what keeps track of every object")]
    public List<sd_ObjectHandler> objectInformation;

    void Awake()
    {
        current = this;
    }

    public void compileObjects(bool start)
    {
        if (start)
        {
            objectInformation.Clear();
            SD_Island.current.island.OHList.Clear();
            for (int i = 0; i < objectStart.Count; i++)
            {
                SD_Island.current.island.OHList.Add(objectStart[i]);
            }
        }
        else
        {
            objectInformation.Clear();
            GameObject[] objectList = GameObject.FindGameObjectsWithTag("object");
            foreach (GameObject creature in objectList)
            {
                creature.GetComponent<Building>().Save();
            }
            SD_Island.current.island.OHList.Clear();
            for (int i = 0; i < objectInformation.Count; i++)
            {
                SD_Island.current.island.OHList.Add(objectInformation[i]);
            }
        }
    }
}
