using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EC_mainWidget : MonoBehaviour
{
    public static EC_mainWidget current;
    public GameObject ECInterface;
    [Header("UI set up")]
    public creatureHandler ch;
    public List<GameObject> creaturePrefabs;
    public List<GameObject> Icons1;
    public List<GameObject> Icons2;
    public Slider slider1;
    public Slider slider2;
    public GameObject breedScreen;
    public GameObject IconSet1;
    public GameObject IconSet2;
    [Header("audio things")]
    public AudioSource AS;
    public AudioClip[] Ac;
    [Header("private varibles")]
    [SerializeField] bool isBreeding;
    [SerializeField] creatureData Creature1;
    [SerializeField] string creature1_E;
    [SerializeField] creatureData Creature2;
    [SerializeField] string creature2_E;

    #region unity voids

    void Awake()
    {
        current = this;
        foreach (creatureData cd in ch.creatureObjects)
        {
            creaturePrefabs.Add(cd.PrefabObj);
        }
    }
    void Update()
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
                resetVars();
            }
        }
        else
        {
            //once sound is made have the audio sourcer play the bad sound
        }
        ECInterface.SetActive(false);
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
        //int random = Random.Range(-1, 1);
        //remember that the breed is cap sensitive and there is a space before, between, and after every elemental letter
        if ((creature1_E == " A " || creature2_E == " A ") && (creature1_E == " B " || creature2_E == " B "))
        {
            GridBuildingSystem.current.InitializeWithBuilding(creaturePrefabs[2]);
        }
    }
    void resetVars()
    {
        creature1_E = null;
        creature2_E = null;
        Creature1 = null;
        Creature2 = null;
    }
    #endregion
}
