using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, ICharacter
{
    private bool isActiveTurn;
    private bool isStunned;
    private int damage;
    private int health;
    private int speed;
    private bool hasFlag;
    private Vector3 position;
    public OverlayTile activeTile;

    //private List<Item> inventory;

    public bool IsActiveTurn { get; set; }
    public bool IsStunned { get; set; }
    public int Damage { get; }
    public int Health { get; set; }
    public int Speed { get; }
    public bool HasFlag { get; set; }
    public Vector3 Position { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
