using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandaid : Abilities
{
    //[SerializeField] private GameObject bananaObject;
    [SerializeField] private int healAmount;

    public override void ExecuteAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.character.Health += healAmount;
        Debug.Log(mouseControllerRef.character.Health);
        currentCooldown = cooldownTurns;
        DeselectAbility();
    }

    public override void SelectAbility()
    {
        ExecuteAbility();
    }
}
