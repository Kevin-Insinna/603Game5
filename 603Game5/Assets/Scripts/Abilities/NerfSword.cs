using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfSword : Abilities
{
    [SerializeField] private int range;
    [SerializeField] private int damage;
    public BattleManager battleManagerRef;

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
        //Deal Damage
        battleManagerRef = BattleManager.Instance;
        foreach(Enemy e in battleManagerRef.enemyList)
        {

            //Debug.Log("Chosen tile location + " + chosenT);
            if(e.activeTile == chosenTile.GetComponent<OverlayTile>())
            {
                e.TakeDamage(damage);
            }
        }

        if (mouseControllerRef.character.MovementLeft > 0)
        {
            mouseControllerRef.character.CanMove = true;
        }

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
