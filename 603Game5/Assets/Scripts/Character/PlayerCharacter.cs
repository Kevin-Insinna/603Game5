using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCharacter : MonoBehaviour, ICharacter
{
    private bool isActiveTurn;
    private bool isStunned;
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private int speed;
    [SerializeField] private Vector3 spawnLocation;
    private bool hasFlag;
    private Vector3 position;
    public OverlayTile activeTile;
    private bool canMove;

    //private List<Item> inventory;

    public bool IsActiveTurn { get; set; }
    public bool IsStunned { get; set; }
    public int Damage { get; }
    public int Health { get; set; }
    public int Speed { get { return speed; } }
    public bool HasFlag { get; set; }
    public Vector3 Position { get; set; }

    public bool CanMove { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("player starting speed" + speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(/*TargetLocation*/)
    {

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
