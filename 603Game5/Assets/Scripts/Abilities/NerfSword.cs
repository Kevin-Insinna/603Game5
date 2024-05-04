using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfSword : Abilities
{
    public BattleManager battleManagerRef;

    public override void SelectAbility()
    {
        mouseControllerRef = MouseController.Instance;
        mouseControllerRef.GetInRangeTiles(range);
    }
}
