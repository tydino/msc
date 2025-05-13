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

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    static Dictionary<TileTypes, TileBase> tileBases = new Dictionary<TileTypes, TileBase>();

    public Building temp;
    Vector3 prevPos;
    BoundsInt prevarea;

    #region Unity Methods

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        string TilePath = @"Tiles/";
        tileBases.Add(TileTypes.Empty, null);
        tileBases.Add(TileTypes.White, Resources.Load<TileBase>(TilePath + "white"));
        tileBases.Add(TileTypes.Red, Resources.Load<TileBase>(TilePath + "Red"));
        tileBases.Add(TileTypes.Green, Resources.Load<TileBase>(TilePath + "Green"));
    }

    void Update()
    {
        if (!temp)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }

            if (!temp.Placed)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

                if (prevPos != cellPos)
                {
                    temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f));
                    prevPos = cellPos;
                    FollowBuilding();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (temp.CanBePlaced())
            {
                temp.Place();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearArea();
            Destroy(temp.gameObject);
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
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }

    #endregion

    #region Build placement

    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        FollowBuilding();
    }

    void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevarea.size.x * prevarea.size.y * prevarea.size.z];
        FillTiles(toClear, TileTypes.Empty);
        TempTilemap.SetTilesBlock(prevarea, toClear);
    }

    void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; i++)
        {
            if(baseArray[i] == tileBases[TileTypes.White])
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
        foreach(var b in baseArray)
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
        SetTilesBlock(area, TileTypes.Empty, TempTilemap);
        SetTilesBlock(area, TileTypes.Green, MainTilemap);
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