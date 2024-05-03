using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WhoopieCushion : Abilities
{
    [SerializeField] private GameObject whoopieObject;
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
        mouseControllerRef = MouseController.Instance;
        //List<OverlayTile> path = mouseControllerRef.pathFinder.FindPath(mouseControllerRef.character.activeTile, chosenTile.GetComponent<OverlayTile>(), mouseControllerRef.inRangeTiles, true);
        if (mouseControllerRef.itemPath.Count > 0)
        {
            Instantiate(whoopieObject, chosenTile.transform.position + new Vector3(0, .5f, 0), new Quaternion(0, 0, 0, 0));
            currentCooldown = cooldownTurns;
            DeselectAbility();
        }
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
