using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EC_mainWidget : MonoBehaviour
{
    [Header("this widget is made for a ABCDEZ place")]
    public static EC_mainWidget current;
    [Header("UI set up")]
    creatureHandler ch;
    List<creatureData> creatureDatas = new List<creatureData>();
    List<GameObject> creaturePrefabs = new List<GameObject>();
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
    //save data things
    public static bool inProgress;
    public static Status status;
    public static int creatureDone;
    public static int creature1_Egg;
    public static int creature2_Egg;
    public static DateTime TimerStart;
    public static DateTime TimerEnd;
    [Header("private varibles")]
    [SerializeField] bool isBreeding;
    [SerializeField] creatureData Creature1;
    [SerializeField] string creature1_E;
    [SerializeField] creatureData Creature2;
    [SerializeField] string creature2_E;

    #region enum
    public enum Status {idle, working, complete}
    #endregion

    #region activation
    void OnMouseDown()
    {
        if (status == Status.complete)
        {
            GridBuildingSystem.current.InitializeWithBuilding(creaturePrefabs[creatureDone]);
            interactionHandler.current.OpenUI(false);
            AS.PlayOneShot(Ac[0]);
            creatureDone = 0;
            status = Status.idle;
            inProgress = false;
            SaveData.current.save();
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
        TimeLeft = interactionHandler.current.TimeLeft;
        TimeLeftSlider = interactionHandler.current.TimeLeftSlider;
        TimeLeftObj = interactionHandler.current.TimeLeftObj;
        ch = creatureHandler.current;
        foreach (creatureData cd in ch.creatureObjects)
        {
            creaturePrefabs.Add(cd.PrefabObj);
            creatureDatas.Add(cd);
        }
        setUpEC();
    }
    void Update()
    {
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
                status = Status.working;
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
        if (chance == -1 || chance == 1) chance = UnityEngine.Random.Range(-1, 1);
        if (chance == -1 || chance == 1) chance = UnityEngine.Random.Range(-1, 1);
        if (chance == -1 || chance == 1) chance = UnityEngine.Random.Range(-1, 1);
        //remember that the breed is cap sensitive and there is a space before, between, and after every elemental letter (order A,B,AB,C, AC, BC, ABC, D, AD, BD, CD, ABD, ACD, BCD, ABCD, E, AE, BE, CE, DE, ABE, ACE, ADE, BCE, BDE, CDE, ABCE, ABDE, ACDE, BCDE, ABCDE, Z)
        gtpDoubles();
        gtpTriples();

        resetVars();
    }
    //doubles if statements
    void gtpDoubles()
    {
        if ((creature1_E == " A " || creature2_E == " A ") && (creature1_E == " B " || creature2_E == " B "))
        {
            CreatureChosen(2);
        }
        if ((creature1_E == " A " || creature2_E == " A ") && (creature1_E == " C " || creature2_E == " C "))
        {
            CreatureChosen(4);
        }
        if ((creature1_E == " B " || creature2_E == " B ") && (creature1_E == " C " || creature2_E == " C "))
        {
            CreatureChosen(5);
        }
    }
    //triples if statements
    void gtpTriples()
    {
        if (((creature1_E == " A " || creature2_E == " A ") && (creature1_E == " BC " || creature2_E == " BC ")) || ((creature1_E == " AB " || creature2_E == " AB ") && (creature1_E == " C " || creature2_E == " C ")) || ((creature1_E == " B " || creature2_E == " B ") && (creature1_E == " AC " || creature2_E == " AC ")))
        {
            CreatureChosen(6);
        }
    }
    void CreatureChosen(int CreatureID)
    {
        if (creatureDatas[CreatureID].PrefabObj != null)
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
        if (creatureDone !> 0)
        {
            AS.PlayOneShot(Ac[3]);
        }
    }
    #endregion

    #region timer

    //figure out skip button eventually

    Coroutine lastTimer;
    Coroutine lastDisplay;

    public void SetUpTimer()
    {

        lastDisplay = StartCoroutine(DisplayTime());
    }

    void StartTimer(int i)
    {
        inProgress = true;
        //initialize before data
        creatureData cd = creatureDatas[i];
        //initialize after data
        TimerStart = DateTime.Now;
        int Days = cd.timeInDays;
        int Hours = cd.timeInHours;
        int Minutes = cd.timeInMinutes;
        int Seconds = cd.timeInSeconds;
        TimeSpan time = new TimeSpan(Days, Hours, Minutes, Seconds);
        TimerEnd = TimerStart.Add(time);

        SetUpTimer();
        lastTimer = StartCoroutine(Timer());
    }

    #region iEnumerators
    IEnumerator DisplayTime()
    {
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimerEnd - start;
        double totalSecondsLeft = timeLeft.TotalSeconds;
        double totalSeconds = (TimerEnd - TimerStart).TotalSeconds;
        string text;
        while (status == Status.working)
        {
            text = "";
            interactionHandler.current.TimeLeftSlider.value = 1 - Convert.ToSingle((TimerEnd - DateTime.Now).TotalSeconds / totalSeconds);
            interactionHandler.current.TimeLeftObj.SetActive(true);
            //skipButton.gameObject.SetActive(true);

            if (totalSecondsLeft > 1)
            {
                if (timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h";
                    interactionHandler.current.TimeLeft.text = text;
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if (timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m";
                    interactionHandler.current.TimeLeft.text = text;
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Seconds + "s";
                    interactionHandler.current.TimeLeft.text = text;
                }
                else
                {
                    text += Mathf.FloorToInt((float)totalSecondsLeft) + "s";
                    interactionHandler.current.TimeLeft.text = text;
                }

                totalSecondsLeft -= Time.deltaTime;
                yield return null;

            }
            else
            {
                interactionHandler.current.TimeLeft.text = "Finished";
                //skipButton.gameObject.SetActive(false);
                interactionHandler.current.TimeLeftSlider.value = 1;
                interactionHandler.current.TimeLeftObj.SetActive(false);
                status = Status.complete;
                inProgress = false;
            }
        }    

        yield return null;
    }

    IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secondsToFinished = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));
        Debug.Log("complete!");
    }
    #endregion
#endregion
}
