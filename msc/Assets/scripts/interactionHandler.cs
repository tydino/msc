using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class interactionHandler : MonoBehaviour
{
    public static interactionHandler current;
    public bool canClick;
    public GameObject Clicked;
    public GameObject main;
    public GameObject placement;
    public bool moved;
    public TilemapRenderer tempRend;
    public TilemapRenderer mainRend;
    public GameObject movedObj;
    [Header("elemental combiner")]
    public GameObject ECUI;
    public GameObject completeEC;
    public GameObject ECInterface;
    public GameObject EC;
    public Text TimeLeft;
    public Slider TimeLeftSlider;
    public GameObject TimeLeftObj;
    [Header("mrs incubator")]
    public GameObject MIUI;
    public GameObject completeMI;
    public Text TimeLeft1;
    public Slider TimeLeftSlider1;
    public GameObject TimeLeftObj1;
    [Header("shop")]
    public GameObject shopUI;

    void Update()
    {
        if (EC_mainWidget.status == EC_mainWidget.Status.complete)
        {
            completeEC.SetActive(true);
        }
        else
        {
            completeEC.SetActive(false);
        }
        if (mi_mainWidget.status == mi_mainWidget.Status.complete)
        {
            completeMI.SetActive(true);
        }
        else
        {
            completeMI.SetActive(false);
        }
    }

    void Start()
    {
        canClick = true;
        ECUI.transform.position = EC_mainWidget.current.gameObject.transform.position;
        MIUI.transform.position = mi_mainWidget.current.gameObject.transform.position;
        EC.SetActive(false);
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
    public void OnClick()
    {
        canClick = true;
    }

    public void OpenUI(bool open)
    {
        if (canClick || open)
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
        SaveData.current.save();
    }
    public void p_place()
    {
        GridBuildingSystem.current.place();
    }
    public void p_flip()
    {
        GridBuildingSystem.current.flip();
        SaveData.current.save();
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
        bool _sleep = Clicked.GetComponent<creatureControler>().sleep;
        if (_sleep)
        {
            Clicked.GetComponent<creatureControler>().sleep = false;
        }
        else
        {
            Clicked.GetComponent<creatureControler>().sleep = true;
            GridBuildingSystem.current.sleep();
        }
        SaveData.current.save();
    }
    #endregion
}
