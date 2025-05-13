using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "creature Data")]
public class creatureData : ScriptableObject
{
    public int creatureInIslandID;
    public GameObject PrefabObj;
    public int worthInCoins;
    public bool coins;
    public int worthInDiamonds;
    public bool diamonds;
}
