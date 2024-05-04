using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBars : Abilities
{
    private bool nextToBars;
    

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
        mouseControllerRef.character.SetLocation(mouseControllerRef.character.nearestBars.CrossMonkeyBars(mouseControllerRef.character.transform.position));
        currentCooldown = cooldownTurns;
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
