using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "object Data")]
public class objectData : ScriptableObject
{
    [Header("main sets")]
    public bool canBeBought = true;
    public string objectName;
    public int objectID;
    public GameObject PrefabObj;
    [Header("store based")]
    public GameObject StoreFront;
    public int worthInCoins;
    public bool coins;
    public int worthInDiamonds;
    public bool diamonds;
}
