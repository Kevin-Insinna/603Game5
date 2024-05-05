using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBars : Abilities
{
    private bool nextToBars;


    public override void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0)
    {
        SaveData();
        DeselectAbility();
    }

    public override void ExecuteAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.character.SetLocation(mouseControllerRef.character.nearestBars.CrossMonkeyBars(mouseControllerRef.character.transform.position));
        currentCooldown = cooldownTurns;
        SaveData();
        DeselectAbility();
    }

    public override void SelectAbility()
    {
        mouseControllerRef = MouseController.Instance;
        nextToBars = mouseControllerRef.character.nextToBars;
        if (nextToBars)
        {
            ExecuteAbility();
        }
    }
}
