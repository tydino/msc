using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    public TilemapRenderer tempRend;
    public TilemapRenderer mainRend;
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
            tempRend.enabled = false;
            mainRend.enabled = false;
            p_cancelObj.SetActive(false);
            p_placeObj.SetActive(false);
            p_flipObj.SetActive(false);
            moved = false;
        }
    }
    public void CloseUI()
    {
        tempRend.enabled = false;
        mainRend.enabled = false;
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
            tempRend.enabled = true;
            mainRend.enabled = true;
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
        Currency.coins = Currency.coins + (Clicked.GetComponent<creatureControler>().thisCreature.worthInCoins/4);
        GridBuildingSystem.current.destroy();
        p_cancelObj.SetActive(false);
        p_placeObj.SetActive(false);
        p_flipObj.SetActive(false);
        Cancel();
        EC_mainWidget.current.setUpEC();
    }
    public void Cancel()
    {
        Clicked.transform.position = Clicked.GetComponent<Building>().prevPOS;
        Clicked.GetComponent<Building>().Placed = true;
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
