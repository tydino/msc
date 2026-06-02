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
    public GameObject ECInterface;
    public ECUIObjects ElementaclCombinerInterface;
    [Header("mrs incubator")]
    public GameObject MrsIncubatorUI;
    public MIUIObjects MrsIncubatorUIInterface;
    [System.Serializable]
    public struct MIUIObjects
    {
        public GameObject NothingScreen;
        public GameObject PatienceScreen;
        public Text TimeLeft;
        public Slider TimeLeftSlider;
        public Text CompletedCreatureName;
        public GameObject CompleteScreen;
    }

    [System.Serializable]
    public struct ECUIObjects
    {
        public GameObject NothingScreen;
        public GameObject PatienceScreen;
        public Text TimeLeft;
        public Slider TimeLeftSlider;
        public GameObject CompleteScreen;
    }

    [Header("shop")]
    public GameObject shopUI;
    public GameObject ShopObj;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        canClick = true;
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

    #region TimerUIs
    public void TimeLeftSlider(float time)
    {
        if (Clicked != null)
        {
            if (Clicked.GetComponent<Building>().creature == false)
            {
                if (Clicked.GetComponent<objectControler>().ThisObjectType == objectControler.ObjectTypes.MrsIncubator)
                {
                    MrsIncubatorUIInterface.TimeLeftSlider.value = time;
                }
                else if (Clicked.GetComponent<objectControler>().ThisObjectType == objectControler.ObjectTypes.ElementalCombiner)
                {
                    ElementaclCombinerInterface.TimeLeftSlider.value = time;
                }
            }
        }
    }
    public void TimeLeft(string time)
    {
        if (Clicked != null)
        {
            if (Clicked.GetComponent<Building>().creature == false)
            {
                if (Clicked.GetComponent<objectControler>().ThisObjectType == objectControler.ObjectTypes.MrsIncubator)
                {
                    MrsIncubatorUIInterface.TimeLeft.text = time;
                }
                else if (Clicked.GetComponent<objectControler>().ThisObjectType == objectControler.ObjectTypes.ElementalCombiner)
                {
                    ElementaclCombinerInterface.TimeLeft.text = time;
                }
            }
        }
    }
    #endregion

    #region mrs incubator UI
    public void SetUpMrsIncubatorUI()
    {
        MrsIncubatorUI.SetActive(true);
        MI_Widget temp = MI_Universal.current.tempMI.GetComponent<MI_Widget>();
        if(temp.status == ObjectTimersBase.Status.idle)
        {
            MrsIncubatorUIInterface.NothingScreen.SetActive(true);
            MrsIncubatorUIInterface.PatienceScreen.SetActive(false);
            MrsIncubatorUIInterface.CompleteScreen.SetActive(false);
            temp.PlaySound(MI_Widget.Sounds.nothing);
        }
        if(temp.status == ObjectTimersBase.Status.working)
        {
            MrsIncubatorUIInterface.NothingScreen.SetActive(false);
            MrsIncubatorUIInterface.PatienceScreen.SetActive(true);
            MrsIncubatorUIInterface.CompleteScreen.SetActive(false);
            temp.PlaySound(MI_Widget.Sounds.patience);
        }
        if(temp.status == ObjectTimersBase.Status.complete)
        {
            MrsIncubatorUIInterface.NothingScreen.SetActive(false);
            MrsIncubatorUIInterface.PatienceScreen.SetActive(false);
            MrsIncubatorUIInterface.CompleteScreen.SetActive(true);
            MrsIncubatorUIInterface.CompletedCreatureName.text = creatureHandler.current.creatureObjects[temp.creatureDone].creatureName;
        }
    }

    public void MI_Sell()
    {
        MI_Universal.current.Sell();
        MI_Universal.current.tempMI.GetComponent<MI_Widget>().PlaySound(MI_Widget.Sounds.sell);
    }
    public void MI_Place()
    {
        MI_Universal.current.Place();
        MI_Universal.current.tempMI.GetComponent<MI_Widget>().PlaySound(MI_Widget.Sounds.place);
    }
    public void MI_Close()
    {
        MI_Universal.current.tempMI = null;
    }
    #endregion

    #region Elemental Combiner UI
    public void SetUpElementaclCombinerUI()
    {
        ECInterface.SetActive(true);
        EC_Widget temp = EC_Universal.current.tempEC.GetComponent<EC_Widget>();
        if (temp.status == ObjectTimersBase.Status.idle)
        {
            ElementaclCombinerInterface.NothingScreen.SetActive(true);
            ElementaclCombinerInterface.PatienceScreen.SetActive(false);
            ElementaclCombinerInterface.CompleteScreen.SetActive(false);
            EC_Universal.current.setUpEC();
        }
        if (temp.status == ObjectTimersBase.Status.working)
        {
            ElementaclCombinerInterface.NothingScreen.SetActive(false);
            ElementaclCombinerInterface.PatienceScreen.SetActive(true);
            ElementaclCombinerInterface.CompleteScreen.SetActive(false);
        }
        if (temp.status == ObjectTimersBase.Status.complete)
        {
            ElementaclCombinerInterface.NothingScreen.SetActive(false);
            ElementaclCombinerInterface.PatienceScreen.SetActive(false);
            ElementaclCombinerInterface.CompleteScreen.SetActive(true);
        }
    }

    public void ECClose()
    {
        EC_Universal.current.tempEC = null;
    }

    public void ECSendCreature()
    {
        EC_Universal.current.SendToMrsIncubator();
        ECClose();
    }
    #endregion

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
            SetUpElementaclCombinerUI();
        }
        #endregion
        #region Mrs Incubator
        if (OC.ThisObjectType == objectControler.ObjectTypes.MrsIncubator)
        {
            MI_Universal.current.tempMI = Clicked.gameObject;
            SetUpMrsIncubatorUI();
        }
        #endregion
    }
    #endregion
}
