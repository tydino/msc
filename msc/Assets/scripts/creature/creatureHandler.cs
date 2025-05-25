using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureHandler : MonoBehaviour
{
    public static creatureHandler current;
    public SD_Island islandSaveData;
    [Header("add 1 to list index for creature id")]
    public List<creatureData> creatureObjects; //have in elemental order: A, B, AB, C, AC, BC, ABC, etc
    public List<sd_CreatureHandler> creatureInformation;

    void Awake()
    {
        current = this;
    }

    public void compileCreatures()
    {
        creatureInformation.Clear();
        GameObject[] creatureList = GameObject.FindGameObjectsWithTag("creature");
        foreach (GameObject creature in creatureList)
        {
            creature.GetComponent<Building>().Save();
        }
        islandSaveData.island.CHList.Clear();
        for(int i=0; i < creatureInformation.Count;i++)
        {
            islandSaveData.island.CHList.Add(creatureInformation[i]);
        }
    }
}
