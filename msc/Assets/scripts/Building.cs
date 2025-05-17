using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed;
    public int ID;
    public BoundsInt area;
    void Start()
    {
        GridBuildingSystem.current.temp = this;
    }

    #region build Methods

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
        GridBuildingSystem.current.TakeArea(areaTemp);
        EC_mainWidget.current.setUpEC();
    }

    public void Save()
    {
        sd_CreatureHandler ch = new sd_CreatureHandler();
        ch.XPos = gameObject.transform.position.x;
        ch.YPos = gameObject.transform.position.y;
        ch.XScl = gameObject.transform.localScale.x;
        ch.CreatureID = ID;
        ch.asleep = gameObject.GetComponent<creatureControler>().sleep;
        creatureHandler CreatureH = GameObject.FindWithTag("CreatureHandler").GetComponent<creatureHandler>();
        CreatureH.creatureInformation.Add(ch);
    }

    #endregion
    #region interaction
    void OnMouseDown()
    {
        if (interactionHandler.current.Clicked == null)
        {
            interactionHandler.current.Clicked = this.gameObject;
        }
    }
    #endregion
}
