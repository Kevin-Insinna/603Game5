using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static Abilities;

public class MouseController : MonoBehaviour
{
    //Map backend
    public Vector3 worldPosition;
    public LayerMask layersToHit;
    private bool tileIsHit;

    //Active character reference
    public GameObject characterPrefab;
    public PlayerCharacter character;

    //Pause Screen
    public PauseScreenBehavior pauseScript;
    public GameObject endTurnButton;

    //Map 
    public PathFinder pathFinder;
    public List<OverlayTile> path = new List<OverlayTile>();

    public RangeFinder rangeFinder;
    public List<OverlayTile> inRangeTiles = new List<OverlayTile>();

    //Ability Buttons
    public List<Button> abilityButtonList = new List<Button>();


    //Setting up mouse controller instance
    private static MouseController _instance;
    public static MouseController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //character = characterPrefab.GetComponent<PlayerCharacter>();
        pathFinder = new PathFinder();
        rangeFinder = new RangeFinder();
        ToggleCursor(true);


        //movementLeft = charac
        character.activeTile = GetActiveTile();
        GetInRangeTiles(character.MovementLeft);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit? focusedTileHit = null;
        
        if (!pauseScript.isPaused)
        {
            focusedTileHit = GetFocusedOnTile();
        }

        //Debug.Log(character.isActiveTurn);
        if (focusedTileHit.HasValue && character.isActiveTurn)
        {
            //Debug.Log("This is running");
            GameObject overlayTile = focusedTileHit.Value.collider.gameObject;
            transform.position = overlayTile.transform.position;
            transform.position = new Vector3(transform.position.x, 0.52f, transform.position.z);

            if (overlayTile.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

                if (Input.GetMouseButtonDown(0))
                {
               
                    //overlayTile.GetComponent<OverlayTile>().ShowTile();
                    if(character.selectedAbility != null)
                    {
                        //Execute ability
                        if (character.selectedAbility.type == AbilityType.Environment)
                        {
                            //Debug.Log("Executed ability");
                            character.selectedAbility.ExecuteAbility(overlayTile, 0);

                        }                   
                    }
                    else
                    {
                        if(character.MovementLeft > 0)
                        {
                            path = pathFinder.FindPath(character.activeTile, overlayTile.GetComponent<OverlayTile>(), inRangeTiles, true);
                            character.CanMove = false;
                        }       
                    }
                }
            }
        }
        
    }

    public void GetInRangeTiles(int tileRange)
    {
        OverlayTile currentTile = GetActiveTile();
        //Debug.Log(currentTile.gridLocation);

        foreach (var item in inRangeTiles)
        {
            item.HideTile();
        }

        inRangeTiles = rangeFinder.GetTilesInRange(currentTile, tileRange);
        //Debug.Log("Active tile: " + currentTile.gridLocation);
        //Debug.Log("Tile range: " + tileRange);
        //Debug.Log(inRangeTiles);

        foreach (var item in inRangeTiles)
        {
            item.ShowTile();
        }

        ToggleCursor(true);
    }

   /* private void MoveAlongPath()
    {
        endTurnButton.SetActive(false);
        BlockTile(false);
        var step = movementSpeed * Time.deltaTime;

        Vector3 tempPathLocation = new Vector3(path[0].transform.position.x, 0.82f, path[0].transform.position.z);
        character.transform.position = Vector3.MoveTowards(character.transform.position, tempPathLocation, step);

        if (Vector3.Distance(character.transform.position, tempPathLocation) < .01f)
        {
            PositionCharacterOnMap(path[0]);
            path.RemoveAt(0);
            movementLeft--;
        }

        if (path.Count == 0)
        {
            //GetInRangeTiles();
            //ToggleCursor(false);
            HideCurrentTiles();
            BlockTile(true);

            if(movementLeft > 0)
            {
                GetInRangeTiles(character.activeTile, movementLeft);
                character.CanMove = true;
            }
            else
            {
                ToggleCursor(false);
            }
        }
    }*/

    public RaycastHit? GetFocusedOnTile()
    {
        Vector3 screenPosition = Input.mousePosition;

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

    public void PositionCharacterOnMap(OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, 0.82f, tile.transform.position.z);
        character.activeTile = tile;
    }

    public void BlockTile(bool isBlocked)
    {
        character.activeTile.isBlocked = isBlocked;
    }

    public OverlayTile GetActiveTile()
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
/*
        Debug.Log("Char Position: " + character.transform.position.x + "Xvalue:" + xValue);
        Debug.Log("Char Position: " + character.transform.position.z + "Zvalue:" + zValue);*/


        Vector2Int locationToCheck = new Vector2Int(xValue, zValue);
        //Debug.Log(locationToCheck);    
        if (map.ContainsKey(locationToCheck))
        {
            return map[locationToCheck];
        }
        else
            return null;
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

    public void SelectAbility(int index)
    {
        character.selectedAbility = character.abilityList[index];
        character.selectedAbility.SelectAbility();
        abilityButtonList[index].interactable = false;
    }

    public void UpdateButtons()
    {
        //Debug.Log("current character " + character);
        for (int i = 0; i < abilityButtonList.Count; i++)
        {
            //Debug.Log("Iteration "+ i);
            //Debug.Log("Current cooldown" + character.abilityList[i].currentCooldown);
            if (character.abilityList[i] != null)
            {
                if (character.abilityList[i].currentCooldown == 0)
                {
                    abilityButtonList[i].interactable = true;
                }
                else
                {
                    abilityButtonList[i].interactable = false;
                }
            }
        }
    }

    public void AbilityUpdateTurn()
    {
        foreach(Abilities a in character.abilityList)
        {
            if(a.currentCooldown > 0)
            {
                a.currentCooldown--;
            }
        }
    }
}
