using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "creature Data")]
public class creatureData : ScriptableObject
{
    [Header("main sets")]
    public string creatureName;
    public int creatureInIslandID;
    public Sprite psd;
    public GameObject PrefabObj;
    [Header("store based")]
    public GameObject StoreFront;
    public int worthInCoins;
    public bool coins;
    public int worthInDiamonds;
    public bool diamonds;
    [Header("must be alphabetical order")]
    public string[] element;
    [Header("elemental combiner based things")]
    public GameObject BreedScreen;
    public Sprite egg;
    public int timeInDays;
    public int timeInHours;
    public int timeInMinutes;
    public int timeInSeconds;
}

