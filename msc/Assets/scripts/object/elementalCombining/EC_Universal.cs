using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EC_Universal : MonoBehaviour
{
    public static EC_Universal current;
    public List<GameObject> ECs;

    [Header("Outward Things")]
    public interactionHandler IH;

    [Header("Do not touch directly")]
    public GameObject tempEC;

    [Header("UI things")]
    public List<GameObject> Icons1;
    public List<GameObject> Icons2;
    public Slider slider1;
    public Slider slider2;
    public GameObject breedScreen;
    public GameObject IconSet1;
    public GameObject IconSet2;
    public int chance;

    [Header("Choices")]
    public List<EC_Choice> ChoicesA = new List<EC_Choice>();

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        slider1.maxValue = Icons1.Count - 1;
        slider2.maxValue = Icons2.Count - 1;
        for (int i = 0; i < Icons1.Count; i++)
        {
            if (slider1.value == i)
            {
                Icons1[i].SetActive(true);
            }
            else
            {
                Icons1[i].SetActive(false);
            }
        }
        for (int i = 0; i < Icons2.Count; i++)
        {
            if (slider2.value == i)
            {
                Icons2[i].SetActive(true);
            }
            else
            {
                Icons2[i].SetActive(false);
            }
        }
    }

    public void setUpEC()
    {
        GameObject[] ECEXE = GameObject.FindGameObjectsWithTag("EC");
        foreach (GameObject ECDestroy in ECEXE)
        {
            Destroy(ECDestroy);
        }
        foreach (GameObject i in Icons1)
        {
            Destroy(i);
        }
        foreach (GameObject i in Icons2)
        {
            Destroy(i);
        }
        Icons1.Clear();
        Icons2.Clear();
        GameObject[] creatureList = GameObject.FindGameObjectsWithTag("creature");
        foreach (GameObject creature in creatureList)
        {
            GameObject temp = Instantiate(creature.GetComponent<creatureControler>().thisCreature.BreedScreen);
            temp.transform.SetParent(IconSet1.transform, false);
            temp.GetComponent<EC_button>().OneOrTwo = 1;
            Icons1.Add(temp);
            temp = Instantiate(creature.GetComponent<creatureControler>().thisCreature.BreedScreen);
            temp.transform.SetParent(IconSet2.transform, false);
            temp.GetComponent<EC_button>().OneOrTwo = 2;
            Icons2.Add(temp);
        }
    }

    public void Button(creatureData cd, int OneOrTwo)
    {
        if (OneOrTwo == 1)
        {
            tempEC.GetComponent<EC_Widget>().Creature1 = cd;
            findElement(true);
}
        if (OneOrTwo == 2)
        {
            tempEC.GetComponent<EC_Widget>().Creature2 = cd;
            findElement(false);
        }
    }

    public void Submit()
    {
        if (tempEC.GetComponent<EC_Widget>().Creature1 != null && tempEC.GetComponent<EC_Widget>().Creature2 != null)
        {
            if (tempEC.GetComponent<EC_Widget>().Creature1 != tempEC.GetComponent<EC_Widget>().Creature2)
            {
                GoThroughPossibilities();
                interactionHandler.current.ECInterface.SetActive(false);
                interactionHandler.current.canClick = true;
                SaveData.current.save();
            }
            else
            {
                tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_YouDidBad);
                resetVars();
            }
        }
        else
        {
            tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_YouDidBad);
        }
    }

    public void SendToMrsIncubator()
    {
        if (MI_Universal.current.FindOneInProgress())
        {
            MI_Universal.current.AvailableMI.GetComponent<MI_Widget>().creatureDone = tempEC.GetComponent<EC_Widget>().creatureDone - 1;
            MI_Universal.current.AvailableMI.GetComponent<MI_Widget>().StartTimer(tempEC.GetComponent<EC_Widget>().creatureDone);
            tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_Collect);
            Debug.Log("Elemental Combiner sent: " + tempEC.GetComponent<EC_Widget>().creatureDone);
            tempEC.GetComponent<EC_Widget>().creatureDone = 0;
            tempEC.GetComponent<EC_Widget>().status = ObjectTimersBase.Status.idle;
            tempEC.GetComponent<EC_Widget>().inProgress = false;
            SaveData.current.save();
        }
        else
        {
            tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_YouDidBad);
        }
    }

    void findElement(bool One)
    {
        if (One)
        {
            tempEC.GetComponent<EC_Widget>().creature1_E = " ";
            foreach (string E in tempEC.GetComponent<EC_Widget>().Creature1.element)
            {
                tempEC.GetComponent<EC_Widget>().creature1_E += E + " ";
            }
        }
        else
        {
            tempEC.GetComponent<EC_Widget>().creature2_E = " ";
            foreach (string E in tempEC.GetComponent<EC_Widget>().Creature2.element)
            {
                tempEC.GetComponent<EC_Widget>().creature2_E += E + " ";
            }
        }
    }
    void GoThroughPossibilities()
    {
        //remember that the breed is cap sensitive and there is a space before, between, and after every elemental letter (order A,B,AB,C, AC, BC, ABC, D, AD, BD, CD, ABD, ACD, BCD, ABCD, E, AE, BE, CE, DE, ABE, ACE, ADE, BCE, BDE, CDE, ABCE, ABDE, ACDE, BCDE, ABCDE, Z)
        foreach (EC_Choice ChoicesB in ChoicesA)
        {
            if ((ChoicesB.choiceOne == tempEC.GetComponent<EC_Widget>().creature1_E || ChoicesB.choiceOne == tempEC.GetComponent<EC_Widget>().creature2_E) && (ChoicesB.choiceTwo == tempEC.GetComponent<EC_Widget>().creature1_E || ChoicesB.choiceTwo == tempEC.GetComponent<EC_Widget>().creature2_E))
            {
                tempEC.GetComponent<EC_Widget>().creature1_Egg = tempEC.GetComponent<EC_Widget>().Creature1.creatureInIslandID - 1;
                tempEC.GetComponent<EC_Widget>().creature2_Egg = tempEC.GetComponent<EC_Widget>().Creature2.creatureInIslandID - 1;
                Debug.Log("Legal: " + ChoicesB.CreatureIDOut);
                CreatureChosen(ChoicesB.CreatureIDOut, true);
            }
            else
            {
                tempEC.GetComponent<EC_Widget>().creature1_Egg = tempEC.GetComponent<EC_Widget>().Creature1.creatureInIslandID - 1;
                tempEC.GetComponent<EC_Widget>().creature2_Egg = tempEC.GetComponent<EC_Widget>().Creature2.creatureInIslandID - 1;
                Debug.Log("Was Illegal");
                CreatureChosen(0, false);
            }
        }

        resetVars();
    }
    void CreatureChosen(int CreatureID, bool legal)
    {
        tempEC.GetComponent<EC_Widget>().status = ObjectTimersBase.Status.working;
        if (legal)
        {
            chance = UnityEngine.Random.Range(-1, 1);
            if (creatureHandler.current.creatureObjects[CreatureID].PrefabObj != null)
            {
                if (chance == 0)
                {
                    tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_Success);
                    tempEC.GetComponent<EC_Widget>().creatureDone = CreatureID;
                    tempEC.GetComponent<EC_Widget>().StartTimer(CreatureID);
                }
                if (chance == 1)
                {
                    tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_Failure);
                    tempEC.GetComponent<EC_Widget>().creatureDone = tempEC.GetComponent<EC_Widget>().Creature2.creatureInIslandID;
                    tempEC.GetComponent<EC_Widget>().StartTimer(tempEC.GetComponent<EC_Widget>().Creature2.creatureInIslandID);
                }
                if (chance == -1)
                {
                    tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_Failure);
                    tempEC.GetComponent<EC_Widget>().creatureDone = tempEC.GetComponent<EC_Widget>().Creature1.creatureInIslandID;
                    tempEC.GetComponent<EC_Widget>().StartTimer(tempEC.GetComponent<EC_Widget>().Creature1.creatureInIslandID);
                }
                SaveData.current.save();
            }
        }
        else
        {
            chance = UnityEngine.Random.Range(0, 1);
            if (chance == 1)
            {
                tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_Failure);
                tempEC.GetComponent<EC_Widget>().creatureDone = tempEC.GetComponent<EC_Widget>().Creature2.creatureInIslandID;
                tempEC.GetComponent<EC_Widget>().StartTimer(tempEC.GetComponent<EC_Widget>().Creature2.creatureInIslandID);
            }
            if (chance == 0)
            {
                tempEC.GetComponent<EC_Widget>().AS.PlayOneShot(tempEC.GetComponent<EC_Widget>().A_Failure);
                tempEC.GetComponent<EC_Widget>().creatureDone = tempEC.GetComponent<EC_Widget>().Creature1.creatureInIslandID;
                tempEC.GetComponent<EC_Widget>().StartTimer(tempEC.GetComponent<EC_Widget>().Creature1.creatureInIslandID);
            }
            SaveData.current.save();
        }
    }
    void resetVars()
    {
        tempEC.GetComponent<EC_Widget>().creature1_E = null;
        tempEC.GetComponent<EC_Widget>().creature2_E = null;
        tempEC.GetComponent<EC_Widget>().Creature1 = null;
        tempEC.GetComponent<EC_Widget>().Creature2 = null;
    }
}

[System.Serializable]
public class EC_Choice
{
    public String choiceOne;
    public String choiceTwo;
    public int CreatureIDOut;
}