using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    bool IsActiveTurn { get; set; }
    bool IsStunned { get; set; }
    int Damage { get; }
    int Health { get; set; }
    int TileRange { get; }
    bool HasFlag { get; set; }
    Vector3 Position { get; set; }
    //Tilemap reference to location instead of just Vector3?

    void Move(/*TargetLocation*/);
    void TakeDamage(int damage);
    void Attack(ICharacter target);
    void EndTurn();
}
