using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class interactionHandler : MonoBehaviour
{
    public static interactionHandler current;
    public GameObject Clicked;
    public GameObject main;
    public GameObject placement;
    public bool moved;
    TilemapRenderer tempRend;
    TilemapRenderer mainRend;
    GameObject movedObj;

    void Start()
    {
        tempRend = GridBuildingSystem.current.TempTilemap.GetComponent<TilemapRenderer>();
        mainRend = GridBuildingSystem.current.MainTilemap.GetComponent<TilemapRenderer>();
        this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        current = this;
        CloseUI();
    }

    public void moveUI()
    {
        Vector3 screenPos = Clicked.transform.position * 80;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        main.GetComponent<RectTransform>().anchoredPosition = new Vector2(screenPos.x, screenPos.y);
    }

    #region voids
    public void OpenUI()
    {
        moveUI();

        main.SetActive(true);
        if (movedObj != Clicked)
        {
            tempRend.enabled = false;
            mainRend.enabled = false;
            placement.SetActive(false);
            moved = false;
        }
    }
    public void CloseUI()
    {
        tempRend.enabled = false;
        mainRend.enabled = false;
        main.SetActive(false);
        placement.SetActive(false);
        moved = false;
    }
    public void CheckMove()
    {
        if (moved)
        {
            Clicked.GetComponent<Building>().Placed = true;
            GridBuildingSystem.current.temp = null;
            tempRend.enabled = false;
            mainRend.enabled = false;
            placement.SetActive(false);
            moved = false;
        }
        else
        {
            tempRend.enabled = true;
            mainRend.enabled = true;
            placement.SetActive(true);
            Move();
            movedObj = Clicked;
            moved = true;
        }
    }
    public void p_cancel()
    {
        Currency.coins = Currency.coins + (Clicked.GetComponent<creatureControler>().thisCreature.worthInCoins/4);
        GridBuildingSystem.current.destroy();
        placement.SetActive(false);
        Cancel();
        EC_mainWidget.current.setUpEC();
    }
    public void p_place()
    {
        GridBuildingSystem.current.place();
    }
    public void p_flip()
    {
        GridBuildingSystem.current.flip();
    }
    public void Cancel()
    {
        Clicked.transform.position = Clicked.GetComponent<Building>().prevPOS;
        Clicked.GetComponent<Building>().Placed = true;
        GridBuildingSystem.current.temp = null;
        GridBuildingSystem.current.ClearArea();
        Clicked = null;
        CloseUI();
    }
    public void Move()
    {
        Clicked.GetComponent<Building>().Placed = false;
        GridBuildingSystem.current.temp = Clicked.GetComponent<Building>();
        GridBuildingSystem.current.FollowBuilding();
    }
    public void Sleep()
    {
        GridBuildingSystem.current.sleep();
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
