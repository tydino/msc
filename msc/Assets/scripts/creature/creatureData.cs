using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "creature Data")]
public class creatureData : ScriptableObject
{
    public int creatureInIslandID;
    public GameObject PrefabObj;
    public GameObject StoreFront;
    public int worthInCoins;
    public bool coins;
    public int worthInDiamonds;
    public bool diamonds;
    [Header("must be alphabetical order")]
    public string[] element;
    [Header("elemental combiner based things")]
    public GameObject BreedScreen;
    public GameObject egg;
    public int timeInDays;
    public int timeInHours;
    public int timeInMinutes;
    public int timeInSeconds;
}

