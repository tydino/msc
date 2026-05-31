using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControler : MonoBehaviour
{
    //Universal
    public GameObject ThisObject;
    public ObjectTypes ThisObjectType;

    //Elemental Combiner
    public EC_Widget ElementalCombinerWidget;

    //Mrs Incubator

    //Numster

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
