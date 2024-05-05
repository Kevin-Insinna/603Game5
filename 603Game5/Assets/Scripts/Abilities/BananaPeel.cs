using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BananaPeel : Abilities
{
    [SerializeField] private GameObject bananaObject;

    public override void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0)
    {
        mouseControllerRef = MouseController.Instance;
        if (mouseControllerRef.itemPath.Count > 0)
        {
            Instantiate(bananaObject, chosenTile.transform.position + new Vector3(0, .5f, 0), new Quaternion(0, 0, 0, 0));
            currentCooldown = cooldownTurns;
            SaveData();
            DeselectAbility();
        }
    }


    public override void SelectAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.GetInRangeTiles(range);
    }
}
