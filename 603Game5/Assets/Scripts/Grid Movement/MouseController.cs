using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    public Vector3 worldPosition;
    public LayerMask layersToHit;
    private bool tileIsHit;
    //private bool isCursorActive;

    public GameObject characterPrefab;
    public PlayerCharacter character;

    private PathFinder pathFinder;
    private List<OverlayTile> path = new List<OverlayTile>();

    private RangeFinder rangeFinder;
    private List<OverlayTile> inRangeTiles = new List<OverlayTile>();

    public float speed;

    //private Vector3 tempPathPos;

    // Start is called before the first frame update
    void Start()
    {
        //character = characterPrefab.GetComponent<PlayerCharacter>();
        pathFinder = new PathFinder();
        rangeFinder = new RangeFinder();
        ToggleCursor(true);

        /*
                tileMap.HasTile(tileLocation)
                character.activeTile = */
        //GetActiveTile();
        GetInRangeTiles();

        //tempPathPos = new Vector3();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusedTileHit = GetFocusedOnTile();

        if (focusedTileHit.HasValue && character.IsActiveTurn)
        {

            GameObject overlayTile = focusedTileHit.Value.collider.gameObject;
            transform.position = overlayTile.transform.position;
            transform.position = new Vector3(transform.position.x, 0.52f, transform.position.z);

            if(overlayTile.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

                if (Input.GetMouseButtonDown(0))
                {
                    //overlayTile.GetComponent<OverlayTile>().ShowTile();
                    path = pathFinder.FindPath(character.activeTile, overlayTile.GetComponent<OverlayTile>(), inRangeTiles);
                    character.CanMove = false;
                }
            }
        }

        if(path.Count > 0)
        {
            MoveAlongPath();
        }
        //Scuffed fix to current active tile hovering above group
        else if(character.activeTile != null)
        {
            character.activeTile.transform.position = new Vector3(character.activeTile.transform.position.x, 0.51f, character.activeTile.transform.position.z);
        }
    }

    public void GetInRangeTiles()
    {
        //Debug.Log("Getting in range tiles");

        GetActiveTile();

        foreach (var item in inRangeTiles)
        {
            item.HideTile();
        }

        inRangeTiles = rangeFinder.GetTilesInRange(character.activeTile,character.Speed); 

        //Debug.Log(inRangeTiles.Count);

        foreach (var item in inRangeTiles)
        {
            item.ShowTile();
        }

        ToggleCursor(true);
    }

    private void MoveAlongPath()
    {
        BlockTile(false);
        //Debug.Log(path.Count);
        var step = speed * Time.deltaTime;

        Vector3 tempPathLocation = new Vector3(path[0].transform.position.x, 0.82f, path[0].transform.position.z);
        character.transform.position = Vector3.MoveTowards(character.transform.position, tempPathLocation, step);

        if (Vector3.Distance(character.transform.position, tempPathLocation) < .01f)
        {
            PositionCharacterOnMap(path[0]);
            path.RemoveAt(0);
        }

        if (path.Count == 0)
        {
            //GetInRangeTiles();
            HideCurrentTiles();
            ToggleCursor(false);
            BlockTile(true);
        }
    }

    public RaycastHit? GetFocusedOnTile()
    {
        Vector3 screenPosition = Input.mousePosition;
        //screenPosition.z = Camera.main.nearClipPlane + 1;
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            worldPosition = hit.point;
            tileIsHit = true;

            return hit;
        }
        else
        {
            tileIsHit = false;
            return null;
        }
    }

    private void PositionCharacterOnMap(OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, 0.82f, tile.transform.position.z);
        character.activeTile = tile;

        character.IsActiveTurn = false;
    }

    private void BlockTile(bool isBlocked)
    {
        character.activeTile.isBlocked = isBlocked;
    }

    private void GetActiveTile()
    {
        var map = MapManager.Instance.map;

        //Weird rounding fix for offset grid
        int xValue;
        int zValue;

        //Adjust x value
        if (character.transform.position.x >= 0)
            xValue = -(int)(-character.transform.position.x);
        else if (character.transform.position.x == -0.5)
            xValue = -1;
        else
            xValue = -Mathf.CeilToInt(Mathf.Abs(character.transform.position.x));

        //Adjust z value
        if (character.transform.position.z >= 0)
            zValue = -(int)(-character.transform.position.z);
        else if (character.transform.position.z == -0.5)
            zValue = -1;
        else
            zValue = -Mathf.CeilToInt(Mathf.Abs(character.transform.position.z));

/*        Debug.Log("Char Position: " + character.transform.position.x +"Xvalue:" + xValue);
        Debug.Log("Char Position: " + character.transform.position.z + "Zvalue:" + zValue);*/


        Vector2Int locationToCheck = new Vector2Int(xValue, zValue);
        //Debug.Log(locationToCheck);    
        if (map.ContainsKey(locationToCheck))
        {
            character.activeTile = map[locationToCheck];
        }
    }

    public void HideCurrentTiles()
    {
        foreach (var item in inRangeTiles)
        {
            item.HideTile();
        }
    }

    public void ToggleCursor(bool cursorState)
    {
        if(cursorState)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        else
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

}
