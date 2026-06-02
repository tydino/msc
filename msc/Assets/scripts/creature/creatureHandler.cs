using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureHandler : MonoBehaviour
{
    public static creatureHandler current;
    [Header("add 1 to list index for creature id")]
    public List<creatureData> creatureObjects; //have in elemental order: A, B, AB, C, AC, BC, ABC, etc
    [Header("this is what keeps track of every creature")]
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
        SD_Island.current.island.CHList.Clear();
        for(int i=0; i < creatureInformation.Count;i++)
        {
            SD_Island.current.island.CHList.Add(creatureInformation[i]);
        }
    }

    private void Start()
    {//the choices are reversable
        foreach (creatureData CD in creatureObjects)
        {//these must be in 0 1 2 3 4 order
            if (CD.element.Length != 1)
            {
                if(CD.element.Length == 2)
                {//only one needed
                    EC_Universal.current.ChoicesA.Add(newChoice(" " + CD.element[0] + " ", " " + CD.element[1] + " ", CD.creatureInIslandID));
                }
                if(CD.element.Length == 3)
                {//only 3 needed
                    EC_Universal.current.ChoicesA.Add(newChoice(" " + CD.element[0] + " ", " " + CD.element[1] + " " + CD.element[2] + " ", CD.creatureInIslandID));
                    EC_Universal.current.ChoicesA.Add(newChoice(" " + CD.element[1] + " ", " " + CD.element[0] + " " + CD.element[2] + " ", CD.creatureInIslandID));
                    EC_Universal.current.ChoicesA.Add(newChoice(" " + CD.element[2] + " ", " " + CD.element[0] + " " + CD.element[1] + " ", CD.creatureInIslandID));
                }
                if (CD.element.Length == 4)
                {//Math incomplete for methods of making one
                    Debug.Log("This is incomplete at this time");
                    return;
                }
                /*if (CD.element.Length == 5)
                {

                }*/
            }
        }
    }
    EC_Choice newChoice(string one, string two, int id)
    {
        EC_Choice choice = new EC_Choice();
        choice.choiceOne = one;
        choice.choiceTwo = two;
        choice.CreatureIDOut = id;
        return choice;
    }
}
