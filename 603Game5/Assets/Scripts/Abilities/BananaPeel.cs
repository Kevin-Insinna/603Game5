using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPeel : Abilities
{
    [SerializeField] private GameObject bananaObject;
    [SerializeField] private int range;

    // Start is called before the first frame update
    void Awake()
    {
        //abilityName = "Banana Peel";
        //description = "Stops someone's movement for turn if they walk over it";
        //cost = 2;
        //type = AbilityType.Environment;
        mouseControllerRef = MouseController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0)
    {
        Instantiate(bananaObject, chosenTile.transform.position + new Vector3(0,.5f,0), new Quaternion(0, 0, 0, 0));
        currentCooldown = cooldownTurns;
        DeselectAbility();
    }

    public override void ExecuteAbility()
    {

    }

    public override void SelectAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.GetInRangeTiles(range);
    }
}
