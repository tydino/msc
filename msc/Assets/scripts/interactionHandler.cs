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
    public GameObject sell;
    public GameObject HasUIButton;
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
    public GameObject MrsIncubatorUI;
    [Header("shop")]
    public GameObject shopUI;
    public GameObject ShopObj;

    void Awake()
    {
        current = this;
    }

    void Update()
    {/*
        if (EC_mainWidget.current.status == EC_mainWidget.Status.complete)
        {
            completeEC.SetActive(true);
        }
        else
        {
            completeEC.SetActive(false);
        }
        if (mi_mainWidget.current.status == mi_mainWidget.Status.complete)
        {
            completeMI.SetActive(true);
        }
        else
        {
            completeMI.SetActive(false);
        }*/
    }

    void Start()
    {
        canClick = true;
        ///ECUI.transform.position = EC_mainWidget.current.gameObject.transform.position;
        ///MIUI.transform.position = mi_mainWidget.current.gameObject.transform.position;
        EC.SetActive(false);
        tempRend = GridBuildingSystem.current.TempTilemap.GetComponent<TilemapRenderer>();
        mainRend = GridBuildingSystem.current.MainTilemap.GetComponent<TilemapRenderer>();
        this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
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

    #region clicked UI (if something is clicked)
    public void OpenUI(bool open, bool canDestroy, bool hasUI)
    {
        if (canClick || open)
        {
            moveUI();

            main.SetActive(true);
            if (canDestroy)
            {
                sell.SetActive(true);
            }
            else
            {
                sell.SetActive(false);
            }
            if (HasUIButton == null)
            {
                Debug.Log("Missing has UI button, fix ASAP");
                ///When implementing have the UI allow for any special UI.
            }
            else
            {
                if (hasUI)
                {
                    HasUIButton.SetActive(true);
                }
                else
                {
                    HasUIButton.SetActive(false);
                }
            }
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

    public void SetUpMrsIncubatorUI()
    {

    }

    public void UIOpen()
    {
        objectControler OC = Clicked.GetComponent<objectControler>();
        #region shop
        if (OC.ThisObjectType == objectControler.ObjectTypes.Market)
        {
            shopUI.SetActive(true);

            foreach (shopInside obj in ShopObj.GetComponentsInChildren<shopInside>())
            {
                Destroy(obj.gameObject);
            }

            foreach (creatureData cd in creatureHandler.current.creatureObjects)
            {
                if (cd.creatureInIslandID != -1)
                {
                    GameObject temp = Instantiate(cd.StoreFront);
                    temp.transform.SetParent(ShopObj.transform, false);
                    shopInside.current.Icons.Add(temp);
                    shopInside.current.Open();
                }
            }
        }
        #endregion
        #region ElementalCombiner
        if (OC.ThisObjectType == objectControler.ObjectTypes.ElementalCombiner)
        {
            EC_Universal.current.tempEC = Clicked.gameObject;
            Debug.Log("Elemental combiner is incomplete. FINISH IT SOON!");
        }
        #endregion
        #region Mrs Incubator
        if (OC.ThisObjectType == objectControler.ObjectTypes.MrsIncubator)
        {
            MrsIncubatorUI.SetActive(true);
            MI_Universal.current.tempMI = Clicked.gameObject;
            SetUpMrsIncubatorUI();
        }
        #endregion
    }
    #endregion
}
