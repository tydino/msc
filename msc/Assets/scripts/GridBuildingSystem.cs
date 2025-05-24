using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngineInternal.Input;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public bool startGame = true;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;
    public TileBase Green;
    public TileBase Red;
    public TileBase White;

    public Coroutine pc;

    static Dictionary<TileTypes, TileBase> tileBases = new Dictionary<TileTypes, TileBase>();

    public Building temp;
    Vector3 prevPos;
    BoundsInt prevarea;

    #region Unity Methods

    public void flip()
    {
        StopCoroutine(pc);
        Vector3 ScaleOne = new Vector3(-1, 1, 1);
        Vector3 ScaleTwo = new Vector3(1, 1, 1);
        if (temp.gameObject.transform.localScale == ScaleTwo)
        {
            temp.gameObject.transform.localScale = ScaleOne;
        }
        else
        {
            temp.gameObject.transform.localScale = ScaleTwo;
        }
    }
    public void destroy()
    {
        StopCoroutine(pc);
        //give back price of creature
        ClearArea();
        Destroy(temp.gameObject);
    }
    public void place()
    {
        if (temp.CanBePlaced())
        {
            StopCoroutine(pc);
            temp.Place();
            interactionHandler.current.CheckMove();
            ClearArea();
        }
    }

    public void sleep()
    {
        StopCoroutine(pc);
    }

    void Awake()
    {
        current = this;
        startGame = true;
    }

    void Start()
    {
        /*string TilePath = @"msc\Assets\scripts\Tiles";
        tileBases.Add(TileTypes.Empty, null);
        tileBases.Add(TileTypes.White, Resources.Load<TileBase>(TilePath + "white"));
        tileBases.Add(TileTypes.Red, Resources.Load<TileBase>(TilePath + "red"));
        tileBases.Add(TileTypes.Green, Resources.Load<TileBase>(TilePath + "green"));*/
        tileBases.Add(TileTypes.Empty, null);
        tileBases.Add(TileTypes.White, White);
        tileBases.Add(TileTypes.Red, Red);
        tileBases.Add(TileTypes.Green, Green);
    }

    void Update()
    {
        if (!temp)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                if (!temp.Placed)
                {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

                    if (prevPos != cellPos)
                    {
                        pc = StartCoroutine(fb(cellPos));
                    }
                }
            }

        }
        if (!temp.Placed)
        {
            interactionHandler.current.Clicked = temp.gameObject;
        }
    }

    IEnumerator fb(Vector3Int cellPos)
    {
        yield return new WaitForSeconds(0.5f);
        if (!temp.Placed) 
        {
            temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f));
            prevPos = cellPos;
            FollowBuilding();
            interactionHandler.current.moveUI();
        }
    }

    #endregion

    #region Tilemap management

    static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    static void SetTilesBlock(BoundsInt area, TileTypes type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    static void FillTiles(TileBase[] arr, TileTypes type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }

    #endregion

    #region Build placement

    public void InitializeWithBuilding(GameObject building)
    {
        startGame = false;
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        temp.Started();
        interactionHandler.current.Clicked = temp.gameObject;
        interactionHandler.current.CheckMove();
        FollowBuilding();
    }

    public void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevarea.size.x * prevarea.size.y * prevarea.size.z];
        FillTiles(toClear, TileTypes.Empty);
        TempTilemap.SetTilesBlock(prevarea, toClear);
    }

    public void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileTypes.White])
            {
                tileArray[i] = tileBases[TileTypes.Green];
            }
            else
            {
                FillTiles(tileArray, TileTypes.Red);
                break;
            }
        }

        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevarea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
        foreach (var b in baseArray)
        {
            if (b != tileBases[TileTypes.White])
            {
                Debug.Log("cannot place here");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        //SetTilesBlock(area, TileTypes.Empty, TempTilemap);
        //SetTilesBlock(area, TileTypes.Green, MainTilemap); just removed this functionality as it does break the game a bit.
    }

    #endregion

}

public enum TileTypes
{
    Empty,
    White,
    Green,
    Red
}