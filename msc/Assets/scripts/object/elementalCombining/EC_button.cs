using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_button : MonoBehaviour
{
    [Header("doesnt matter what place")]
    public creatureData cd;
    public int OneOrTwo;
    public void IClicked()
    {
        EC_Universal.current.Button(cd, OneOrTwo);
    }
}
