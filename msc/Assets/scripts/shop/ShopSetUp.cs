using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSetUp : MonoBehaviour
{
    public creatureHandler ch;
    public GameObject ShopObj;
    public shopInside si;

    void Awake()
    {
        foreach(creatureData cd in ch.creatureObjects)
        {
            if (cd.creatureInIslandID != -1)
            {
                GameObject temp = Instantiate(cd.StoreFront);
                temp.transform.SetParent(ShopObj.transform, false);
                si.Icons.Add(temp);
            }
        }
    }

    void OnMouseDown()
    {
        interactionHandler.current.shopUI.SetActive(true);
        interactionHandler.current.canClick = false;
    }
}
