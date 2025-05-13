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
        if (Placed == false)
        {
            sd_CreatureHandler ch = new sd_CreatureHandler();
            ch.XPos = gameObject.transform.position.x;
            ch.YPos = gameObject.transform.position.y;
            ch.XScl = gameObject.transform.localScale.x;
            ch.CreatureID = ID;
            creatureHandler CreatureH = GameObject.FindWithTag("CreatureHandler").GetComponent<creatureHandler>();
            CreatureH.creatureInformation.Add(ch);
        }
        //move these lines above to another script when figure out event system.
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }



    #endregion
}
