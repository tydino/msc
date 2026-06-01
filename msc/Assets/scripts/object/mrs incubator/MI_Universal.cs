using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Universal : MonoBehaviour
{
    public static MI_Universal current;
    public List<GameObject> MIs;
    public GameObject AvailableMI;
    public GameObject tempMI;

    private void Awake()
    {
        current = this;
    }

    public bool FindOneInProgress()
    {
        foreach(GameObject MI in MIs)
        {
            if (!MI.GetComponent<MI_Widget>().inProgress)
            {
                AvailableMI = MI;
                return true;
            }
        }

        return false;
    }

    public void StartTimer(int i)
    {
        AvailableMI.GetComponent<MI_Widget>().StartTimer(i);
        AvailableMI = null;
    }
}
