using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EC_mainWidget : ObjectTimersBase
{
    [Header("this widget is made for a ABCDEZ place")]
    public static EC_mainWidget current;
    [Header("visuals/animation")]
    public SpriteRenderer inputOne;
    public SpriteRenderer inputTwo;
    public SpriteRenderer output;
    public Animator animator;
    public float Tempo;
    int samples = 60;
    [Header("UI set up")]
    creatureHandler ch;
    public List<GameObject> Icons1;
    public List<GameObject> Icons2;
    public Slider slider1;
    public Slider slider2;
    public GameObject breedScreen;
    public GameObject IconSet1;
    public GameObject IconSet2;
    public int chance;
    Text TimeLeft;
    Slider TimeLeftSlider;
    GameObject TimeLeftObj;
    [Header("audio things")]
    public AudioSource AS;
    public AudioClip[] Ac;
    [Header("Choices")]
    public List<EC_Choice> ChoicesA = new List<EC_Choice>();
    [Header("save data things")]
    public static int creatureDone;
    public static int creature1_Egg;
    public static int creature2_Egg;
    [Header("private varibles")]
    [SerializeField] bool isBreeding;
    [SerializeField] creatureData Creature1;
    [SerializeField] string creature1_E;
    [SerializeField] creatureData Creature2;
    [SerializeField] string creature2_E;

    #region activation
    void OnMouseDown()
    {
        if (status == Status.complete)
        {
            if (mi_mainWidget.current.status == mi_mainWidget.Status.idle) 
            {
                mi_mainWidget.current.StartTimer(creatureDone);
                AS.PlayOneShot(Ac[0]);
                creatureDone = 0;
                status = Status.idle;
                inProgress = false;
                SaveData.current.save();
            }
        }
        else
        {
            if (!inProgress)
            {
                interactionHandler.current.canClick = false;
                interactionHandler.current.ECInterface.SetActive(true);
            }
        }
    }
    #endregion

    #region unity voids
    void Awake()
    {
        current = this;
    }

    void Start()
    {
        animator.SetFloat("s", Tempo / samples);
        TimeLeft = interactionHandler.current.TimeLeft;
        TimeLeftSlider = interactionHandler.current.TimeLeftSlider;
        TimeLeftObj = interactionHandler.current.TimeLeftObj;
        ch = creatureHandler.current;
        foreach (creatureData cd in ch.creatureObjects)
        {
            creaturePrefabs.Add(cd.PrefabObj);
            creatureHandler.current.creatureObjects.Add(cd);
        }
        setUpEC();
    }
    void Update()
    {
        if (status == Status.idle)
        {
            animator.SetBool("working", false);
            animator.SetBool("waiting", false);
            inputOne.sprite = null;
            inputTwo.sprite = null;
            output.sprite = null;
        }
        else if(status == Status.working)
        {
            animator.SetBool("working", true);
            animator.SetBool("waiting", false);
            inputOne.sprite = creatureHandler.current.creatureObjects[creature1_Egg].egg;
            inputTwo.sprite = creatureHandler.current.creatureObjects[creature2_Egg].egg;
            output.sprite = null;
        }
        else if(status == Status.complete)
        {
            animator.SetBool("working", false);
            animator.SetBool("waiting", true);
            inputOne.sprite = null;
            inputTwo.sprite = null;
            output.sprite = creatureHandler.current.creatureObjects[creatureDone].egg;
        }
            
        SetUpTimer();
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
    #endregion

    #region elemental combination function

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
        SetUpTimer();
    }
    public void Button(creatureData cd, int OneOrTwo)
    {
        if (OneOrTwo == 1)
        {
            Creature1 = cd;
        }
        if (OneOrTwo == 2)
        {
            Creature2 = cd;
        }
    }
    public void Submit()
    {
        if (Creature1 != null && Creature2 != null)
        {
            if (Creature1 != Creature2)
            {
                GoThroughPossibilities();
                interactionHandler.current.ECInterface.SetActive(false);
                interactionHandler.current.canClick = true;
            }
            else
            {
                resetVars();
            }
        }
    }

    void findElement()
    {
        creature1_E = " ";
        creature2_E = " ";
        foreach (string E in Creature1.element)
        {
            creature1_E += E + " ";
        }
        foreach (string E in Creature2.element)
        {
            creature2_E += E + " ";
        }
    }
    void GoThroughPossibilities()
    {
        findElement();
        chance = UnityEngine.Random.Range(-1, 1);
        //remember that the breed is cap sensitive and there is a space before, between, and after every elemental letter (order A,B,AB,C, AC, BC, ABC, D, AD, BD, CD, ABD, ACD, BCD, ABCD, E, AE, BE, CE, DE, ABE, ACE, ADE, BCE, BDE, CDE, ABCE, ABDE, ACDE, BCDE, ABCDE, Z)
        foreach (EC_Choice ChoicesB in ChoicesA)
        {
            if((ChoicesB.choiceOne == creature1_E || ChoicesB.choiceOne == creature2_E) && (ChoicesB.choiceTwo == creature1_E || ChoicesB.choiceTwo == creature2_E))
            {
                creature1_Egg = Creature1.creatureInIslandID - 1;
                creature2_Egg = Creature2.creatureInIslandID - 1;
                CreatureChosen(ChoicesB.CreatureIDOut);
            }
        }

        resetVars();
    }
    void CreatureChosen(int CreatureID)
    {
        status = Status.working;
        if (creatureHandler.current.creatureObjects[CreatureID].PrefabObj != null)
        {
            if (chance == 0)
            {
                AS.PlayOneShot(Ac[2]);
                creatureDone = CreatureID;
                StartTimer(CreatureID);
            }
            if (chance == 1)
            {
                creatureDone = Creature2.creatureInIslandID;
                StartTimer(Creature2.creatureInIslandID);
                AS.PlayOneShot(Ac[1]);
            }
            if (chance == -1)
            {
                creatureDone = Creature1.creatureInIslandID;
                StartTimer(Creature1.creatureInIslandID);
                AS.PlayOneShot(Ac[1]);
            }
            SaveData.current.save();
        }
    }
    void resetVars()
    {
        creature1_E = null;
        creature2_E = null;
        Creature1 = null;
        Creature2 = null;
        if (creatureDone == 0)
        {
            AS.PlayOneShot(Ac[3]);
        }
    }
    #endregion
}

[System.Serializable]
public class EC_Choice
{
    public String choiceOne;
    public String choiceTwo;
    public int CreatureIDOut;
}
