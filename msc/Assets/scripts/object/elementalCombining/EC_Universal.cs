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
}

[System.Serializable]
public class EC_Choice
{
    public String choiceOne;
    public String choiceTwo;
    public int CreatureIDOut;
}