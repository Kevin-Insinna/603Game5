using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skateboard : Abilities
{
    //[SerializeField] private GameObject bananaObject;
    [SerializeField] private int addedRange;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0)
    {

    }

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
