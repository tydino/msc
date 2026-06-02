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
            if (MI.GetComponent<MI_Widget>().status == ObjectTimersBase.Status.idle)
            {
                AvailableMI = MI;
                return true;
            }
        }

        return false;
    }

    public void StartTimer(int i)
    {//this only runs after finding one in progress Make sure to run the Find One In Progress bool in a if statement first!
        AvailableMI.GetComponent<MI_Widget>().StartTimer(i);
        AvailableMI.GetComponent<MI_Widget>().creatureDone = i;
        AvailableMI = null;
    }

    public void Place()
    {
        GridBuildingSystem.current.InitializeWithBuilding(creatureHandler.current.creatureObjects[tempMI.GetComponent<MI_Widget>().creatureDone].PrefabObj);
        tempMI.GetComponent<MI_Widget>().status = ObjectTimersBase.Status.idle;
        tempMI.GetComponent<MI_Widget>().creatureDone = 0;
        tempMI.GetComponent<MI_Widget>().inProgress = false;
        SaveData.current.save();
    }

    public void Sell()
    {
        Currency.coins = Currency.coins + creatureHandler.current.creatureObjects[tempMI.GetComponent<MI_Widget>().creatureDone].worthInCoins;
        tempMI.GetComponent<MI_Widget>().status = ObjectTimersBase.Status.idle;
        tempMI.GetComponent<MI_Widget>().creatureDone = 0;
        tempMI.GetComponent<MI_Widget>().inProgress = false;
        SaveData.current.save();
    }
}
