using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int tileRange;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float health;
    [SerializeField] private Vector3 spawnLocation;

    [SerializeField] private float currentHealth;
    public OverlayTile activeTile;
    public bool isActiveTurn;
    private bool executeMove;

    [SerializeField] private GameObject target;
    public bool canMove;
    private int spacesMoved;
    private int movementLeft;

    private bool movementEnded;
    private bool isStunned;


    private PathFinder pathFinder;
    private List<OverlayTile> path = new List<OverlayTile>();

    private RangeFinder rangeFinder;
    private List<OverlayTile> inRangeTiles = new List<OverlayTile>();

    //public GameObject battleManagerPrefab;
    public BattleManager battleManager;

    private OverlayTile findTile;

    public GameObject healthBar;


    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new PathFinder();
        rangeFinder = new RangeFinder();

        currentHealth = health;

        isActiveTurn = false;
        executeMove = false;
        canMove = false;

        spacesMoved = 0;
        //movementLeft = tileRange;

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
                StartCoroutine(CanMoveDelay(1.5f));
            }

            if (canMove)
            {
                MoveAlongPath();
            }

        }       
    }

    private void MoveAlongPath()
    {
        //Debug.Log("")
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
            StartCoroutine(EndTurnDelay(1.5f));
            //battleManager.EndTurn();
            return;
        }

        Vector3 tempPathLocation = new Vector3(path[0].transform.position.x, 0.82f, path[0].transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, tempPathLocation, step);

        if (Vector3.Distance(transform.position, tempPathLocation) < .01f)
        {
            PositionCharacterOnMap(path[0]);
            path.RemoveAt(0);
            if (movementEnded)
            {
                spacesMoved = tileRange;
                movementEnded = false;
            }
            else
            {
                spacesMoved++;
            }
        }

        if (spacesMoved == tileRange || path.Count == 0)
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

    private IEnumerator CanMoveDelay(float delay)
    {
        //Debug.Log("courotine running");
        yield return new WaitForSeconds(delay);
        //Debug.Log("courotine done");
        canMove = true;
    }

    private IEnumerator EndTurnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        battleManager.EndTurn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Banana"))
        {
            Destroy(other.gameObject);
            movementEnded = true;
        }
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth = currentHealth - damageTaken;
        if(currentHealth <= 0)
        {
            currentHealth = health;
            transform.position = spawnLocation;
            activeTile = GetActiveTile(this.gameObject);


        }
        Debug.Log("Damage taken " + damageTaken);
        Debug.Log("Current health" + currentHealth);

        healthBar.GetComponent<Image>().fillAmount = currentHealth/health;
    }
}
