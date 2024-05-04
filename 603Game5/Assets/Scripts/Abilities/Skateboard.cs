using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skateboard : Abilities
{
    //[SerializeField] private GameObject bananaObject;
    [SerializeField] private int addedRange;

    public override void ExecuteAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.character.MovementLeft += 2;
        mouseControllerRef.character.CanMove = true;
        mouseControllerRef.GetInRangeTiles(mouseControllerRef.character.MovementLeft);
        currentCooldown = cooldownTurns;
        DeselectAbility();
    }

    public override void SelectAbility()
    {
        ExecuteAbility();
    }
}
