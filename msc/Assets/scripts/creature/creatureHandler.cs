using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureHandler : MonoBehaviour
{
    public SD_Island islandSaveData;
    public List<GameObject> creatureObjects;
    public List<sd_CreatureHandler> creatureInformation;

    public void compileCreatures()
    {
        islandSaveData.island.CHList.Clear();
        for(int i=0; i < creatureInformation.Count;i++)
        {
            islandSaveData.island.CHList.Add(creatureInformation[i]);
        }
    }
}
