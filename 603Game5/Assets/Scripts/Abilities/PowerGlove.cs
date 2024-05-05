using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGlove : Abilities
{
    private bool nextToShelf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0)
    {
        DeselectAbility();
    }

    public override void ExecuteAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.character.nearestShelf.KnockShelf();
        currentCooldown = cooldownTurns;
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
