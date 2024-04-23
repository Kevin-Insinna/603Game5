using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCharacter : MonoBehaviour
{
    public bool isActiveTurn;
    private bool isStunned;

    //Player Stats
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private int tileRange;
    [SerializeField] private int movementSpeed;
    [SerializeField] public List<Abilities> abilityList;
    public Abilities selectedAbility;

    [SerializeField] private Vector3 spawnLocation;

    private bool hasFlag;
    private Vector3 position;
    public OverlayTile activeTile;
    private bool canMove;
    private int movementLeft;

    //private List<Item> inventory;

    public bool IsActiveTurn { get { return isActiveTurn; } set { isActiveTurn = value; } }
    public bool IsStunned { get; set; }
    public int Damage { get; }
    public int Health { get; set; }
    public int TileRange { get { return tileRange; } }

    public int MovementLeft { get { return movementLeft; } set { movementLeft = value; } }

    public bool HasFlag { get; set; }
    public Vector3 Position { get; set; }

    public bool CanMove { get; set; }

    MouseController mouseControllerRef;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("player starting speed" + speed);
        movementLeft = tileRange;
        mouseControllerRef = MouseController.Instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isActiveTurn)
        {
            if (mouseControllerRef.path.Count > 0)
            {
                MoveAlongPath();
            }
        }
    }

    private void MoveAlongPath()
    {
        mouseControllerRef.BlockTile(false);
        var step = movementSpeed * Time.deltaTime;

        Vector3 tempPathLocation = new Vector3(mouseControllerRef.path[0].transform.position.x, 0.82f, mouseControllerRef.path[0].transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, tempPathLocation, step);

        if (Vector3.Distance(transform.position, tempPathLocation) < .01f)
        {
            mouseControllerRef.PositionCharacterOnMap(mouseControllerRef.path[0]);
            mouseControllerRef.path.RemoveAt(0);
            movementLeft--;
        }

        if (mouseControllerRef.path.Count == 0)
        {
            mouseControllerRef.HideCurrentTiles();
            mouseControllerRef.BlockTile(true);

            if (movementLeft > 0)
            {
                mouseControllerRef.GetInRangeTiles(movementLeft);
                canMove = true;
            }
            else
            {
                mouseControllerRef.ToggleCursor(false);
            }
        }
    }

    public void TakeDamage(int damage)
    {

    }

    public void Attack(ICharacter target)
    {

    }

    public void EndTurn()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.position = spawnLocation;
            GetActiveTile();
        }
    }

    private void GetActiveTile()
    {
        var map = MapManager.Instance.map;

        //Weird rounding fix for offset grid
        int xValue;
        int zValue;

        //Adjust x value
        if (transform.position.x >= 0)
            xValue = -(int)(-transform.position.x);
        else if (transform.position.x == -0.5)
            xValue = -1;
        else
            xValue = -Mathf.CeilToInt(Mathf.Abs(transform.position.x));

        //Adjust z value
        if (transform.position.z >= 0)
            zValue = -(int)(-transform.position.z);
        else if (transform.position.z == -0.5)
            zValue = -1;
        else
            zValue = -Mathf.CeilToInt(Mathf.Abs(transform.position.z));

        Vector2Int locationToCheck = new Vector2Int(xValue, zValue);
        if (map.ContainsKey(locationToCheck))
        {
            activeTile = map[locationToCheck];
        }
    }
}
