using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGlove : Abilities
{
    private bool nextToShelf;

    public override void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0)
    {
        SaveData();
        DeselectAbility();
    }

    public override void ExecuteAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.character.nearestShelf.KnockShelf();
        currentCooldown = cooldownTurns;
        SaveData();
        DeselectAbility();
    }

    public override void SelectAbility()
    {
        mouseControllerRef = MouseController.Instance;
        nextToShelf = mouseControllerRef.character.nextToShelf;
        if (nextToShelf)
        {
            ExecuteAbility();
        }
    }
}
