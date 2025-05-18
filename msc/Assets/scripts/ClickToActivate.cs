using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToActivate : MonoBehaviour
{
    public GameObject objToActivate;
    void OnMouseDown()
    {
        objToActivate.SetActive(true);
    }
}
