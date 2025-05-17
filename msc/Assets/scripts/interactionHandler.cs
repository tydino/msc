using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionHandler : MonoBehaviour
{
    public static interactionHandler current;
    public GameObject Clicked;
    public GameObject cancelObj;
    public GameObject moveObj;
    public GameObject sleepObj;
    public GameObject p_cancelObj;
    public GameObject p_placeObj;
    public GameObject p_flipObj;
    bool moved;
    GameObject movedObj;

    void Start()
    {
        current = this;
        CloseUI();
    }
    void Update()
    {
        if (Clicked != null)
        {
            OpenUI();
        }
    }

    #region voids
    public void OpenUI()
    {
        cancelObj.SetActive(true);
        moveObj.SetActive(true);
        sleepObj.SetActive(true);
        if (movedObj != Clicked)
        {
            p_cancelObj.SetActive(false);
            p_placeObj.SetActive(false);
            p_flipObj.SetActive(false);
            moved = false;
        }
    }
    public void CloseUI()
    {
        cancelObj.SetActive(false);
        moveObj.SetActive(false);
        sleepObj.SetActive(false);
        p_cancelObj.SetActive(false);
        p_placeObj.SetActive(false);
        p_flipObj.SetActive(false);
    }
    public void CheckMove()
    {
        if (moved)
        {
            Clicked.GetComponent<Building>().Placed = true;
            GridBuildingSystem.current.temp = null;
            p_cancelObj.SetActive(false);
            p_placeObj.SetActive(false);
            p_flipObj.SetActive(false);
            moved = false;
        }
        else
        {
            p_cancelObj.SetActive(true);
            p_placeObj.SetActive(true);
            p_flipObj.SetActive(true);
            Move();
            movedObj = Clicked;
            moved = true;
        }
    }
    public void p_cancel()
    {
        GridBuildingSystem.current.destroy();
        p_cancelObj.SetActive(false);
        p_placeObj.SetActive(false);
        p_flipObj.SetActive(false);
        Cancel();
    }
    public void Cancel()
    {
        Clicked = null;
        CloseUI();
    }
    public void Move()
    {
        Clicked.GetComponent<Building>().Placed = false;
        GridBuildingSystem.current.temp = Clicked.GetComponent<Building>();
    }
    public void Sleep()
    {
        bool _sleep = Clicked.GetComponent<creatureControler>().sleep;
        if (_sleep)
        {
            Clicked.GetComponent<creatureControler>().sleep = false;
        }
        else
        {
            Clicked.GetComponent<creatureControler>().sleep = true;
        }
    }
    #endregion
}
