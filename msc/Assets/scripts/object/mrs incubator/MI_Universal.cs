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

    public void SkipTimer()
    {
        tempMI.GetComponent<MI_Widget>().SkipTime();
        interactionHandler.current.SetUpMrsIncubatorUI();
    }

    public void Place()
    {
        if (Hotel_universal.current.AvailableSpace(creatureHandler.current.creatureObjects[tempMI.GetComponent<MI_Widget>().creatureDone].bedsNeeded))
        {
            GridBuildingSystem.current.InitializeWithBuilding(creatureHandler.current.creatureObjects[tempMI.GetComponent<MI_Widget>().creatureDone].PrefabObj);
            GridBuildingSystem.current.temp.setClickedToThisObject();
            Hotel_universal.current.RemoveSpace(creatureHandler.current.creatureObjects[tempMI.GetComponent<MI_Widget>().creatureDone].bedsNeeded);
            tempMI.GetComponent<MI_Widget>().status = ObjectTimersBase.Status.idle;
            tempMI.GetComponent<MI_Widget>().creatureDone = 0;
            tempMI.GetComponent<MI_Widget>().inProgress = false;
            SaveData.current.save();
        }
        else
        {
            tempMI.GetComponent<MI_Widget>().PlaySound(MI_Widget.Sounds.patience);
        }
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
