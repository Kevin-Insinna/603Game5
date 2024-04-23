using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemySpeedMax;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int damage;
    [SerializeField] private int health;
    private OverlayTile activeTile;
    public bool isActiveTurn;
    private bool executeMove;

    [SerializeField] private GameObject target;
    public bool canMove;
    private int spacesMoved;

    private PathFinder pathFinder;
    private List<OverlayTile> path = new List<OverlayTile>();

    private RangeFinder rangeFinder;
    private List<OverlayTile> inRangeTiles = new List<OverlayTile>();

    //public GameObject battleManagerPrefab;
    public BattleManager battleManager;

    private OverlayTile findTile;


    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new PathFinder();
        rangeFinder = new RangeFinder();

        isActiveTurn = false;
        executeMove = false;
        canMove = false;

        spacesMoved = 0; 

        activeTile = GetActiveTile(this.gameObject);

        inRangeTiles = rangeFinder.GetTilesInRange(activeTile, 50);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveTurn)
        {
            
            if (!executeMove)
            {
                activeTile = GetActiveTile(this.gameObject);
                //overlayTile.GetComponent<OverlayTile>().ShowTile();
                inRangeTiles = rangeFinder.GetTilesInRange(activeTile, 50);

                findTile = FindTile();

                path = pathFinder.FindPath(activeTile, findTile, inRangeTiles, false);

                executeMove = true;
                canMove = true;
            }

            if (canMove)
            {
                MoveAlongPath();
            }

        }       
    }

    private void MoveAlongPath()
    {
        BlockTile(false);
        var step = movementSpeed * Time.deltaTime;

        if(path.Count < 1)
        {
            path = new List<OverlayTile>();
            activeTile = GetActiveTile(this.gameObject);
            BlockTile(true);
            executeMove = false;
            canMove = false;
            spacesMoved = 0;

            isActiveTurn = false;
            battleManager.EndTurn();
            return;
        }

        Vector3 tempPathLocation = new Vector3(path[0].transform.position.x, 0.82f, path[0].transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, tempPathLocation, step);

        if (Vector3.Distance(transform.position, tempPathLocation) < .01f)
        {
            PositionCharacterOnMap(path[0]);
            path.RemoveAt(0);
            spacesMoved++;
        }

        if (spacesMoved == enemySpeedMax || path.Count == 0)
        {
            path = new List<OverlayTile>();
            activeTile = GetActiveTile(this.gameObject);
            BlockTile(true);
            executeMove = false;
            canMove = false;
            spacesMoved = 0;

            isActiveTurn = false;
            battleManager.EndTurn();
        }
    }

    private OverlayTile FindTile()
    {
        //Will be expanded for more meaningful decision making
        OverlayTile targetTile = GetActiveTile(target);

        return targetTile;
    }

    private OverlayTile GetActiveTile(GameObject targetObject)
    {
        var map = MapManager.Instance.map;

        //Weird rounding fix for offset grid
        int xValue;
        int zValue;

        //Adjust x value
        if (targetObject.transform.position.x >= 0)
            xValue = -(int)(-targetObject.transform.position.x);
        else if (targetObject.transform.position.x == -0.5)
            xValue = -1;
        else
            xValue = -Mathf.CeilToInt(Mathf.Abs(targetObject.transform.position.x));

        //Adjust z value
        if (targetObject.transform.position.z >= 0)
            zValue = -(int)(-targetObject.transform.position.z);
        else if (targetObject.transform.position.z == -0.5)
            zValue = -1;
        else
            zValue = -Mathf.CeilToInt(Mathf.Abs(targetObject.transform.position.z));

        Vector2Int locationToCheck = new Vector2Int(xValue, zValue);
        //Debug.Log(locationToCheck);    
        if (map.ContainsKey(locationToCheck))
        {
            return map[locationToCheck];
        }
        else
        {
            return null;
        }
    }

    private void BlockTile(bool isBlocked)
    {
        activeTile.isBlocked = isBlocked;
    }

    private void PositionCharacterOnMap(OverlayTile tile)
    {
        transform.position = new Vector3(tile.transform.position.x, 0.82f, tile.transform.position.z);
        activeTile = tile;

        //IsActiveTurn = false;
    }
}
