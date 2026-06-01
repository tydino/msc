using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool canBeDestroyed = true;
    public bool hasUI = false;
    public bool creature = true;
    public bool Placed;
    public int ID;
    public BoundsInt area;
    public Vector3 prevPOS;
    void Start()
    {
        prevPOS = transform.position;
    }

    #region build Methods

    public void Started()
    {
        if (creature)
        {
            interactionHandler.current.main.SetActive(true);
            interactionHandler.current.tempRend.enabled = true;
            interactionHandler.current.mainRend.enabled = true;
            interactionHandler.current.placement.SetActive(true);
            interactionHandler.current.Clicked = this.gameObject;
            interactionHandler.current.movedObj = this.gameObject;
            interactionHandler.current.moveUI();
            prevPOS = transform.position;
        }
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        //GridBuildingSystem.current.TakeArea(areaTemp);
        ///EC_mainWidget.current.setUpEC(); FIX THIS
        prevPOS = transform.position;

        SaveData.current.save();
    }

    public void Save()
    {
        if (creature)
        {
            sd_CreatureHandler ch = new sd_CreatureHandler();
            ch.XPos = gameObject.transform.position.x;
            ch.YPos = gameObject.transform.position.y;
            ch.XScl = gameObject.transform.localScale.x;
            ch.CreatureID = ID;
            ch.asleep = gameObject.GetComponent<creatureControler>().sleep;
            creatureHandler.current.creatureInformation.Add(ch);
        }
        else
        {
            sd_ObjectHandler oh = new sd_ObjectHandler();
            oh.XPos = gameObject.transform.position.x;
            oh.YPos = gameObject.transform.position.y;
            oh.XScl = gameObject.transform.localScale.x;
            oh.ObjectID = ID;
            oh.Data = gameObject.GetComponent<objectControler>().CompileData();
            objectHandler.current.objectInformation.Add(oh);
        }
    }

    #endregion
    #region interaction
    void OnMouseDown()
    {
        if (interactionHandler.current.Clicked != this.gameObject && interactionHandler.current.moved == false)
        {
            interactionHandler.current.Clicked = this.gameObject;
            interactionHandler.current.OpenUI(false, canBeDestroyed, hasUI);
        }
    }
    #endregion
}
