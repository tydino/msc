using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControler : MonoBehaviour
{
    public GameObject ThisObject;
    public ObjectTypes ThisObjectType;

    public enum ObjectTypes
    {
        ElementalCombiner,
        MrsIncubator,
        Market,
        Maps,
        Numster
    }

    private void Start()
    {
        ThisObject = gameObject;
    }

    public string CompileData()
    {
        return null;
    }

    public void DecompileData(string Data)
    {

    }
}
