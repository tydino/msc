using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSetUp : MonoBehaviour
{
    public creatureHandler ch;
    public List<creatureData> creatureObjects;
    public GameObject ShopObj;
    public shopInside si;

    void Awake()
    {
        foreach(creatureData cd in ch.creatureObjects)
        {
            GameObject temp = Instantiate(cd.StoreFront);
            temp.transform.SetParent(ShopObj.transform,false);
            si.Icons.Add(temp);
        }
    }
}
