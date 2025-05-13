using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick_clone : MonoBehaviour
{
    public int price;
    public Vector3 posOfCreature;
    public GameObject creatureToClone;

    public void clone()
    {
        GameObject clone = Instantiate(creatureToClone);
        clone.transform.position = posOfCreature;
    }
}
public enum currency{ 
    coins,
    diamonds
}
