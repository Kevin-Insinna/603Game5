using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandaid : Abilities
{
    //[SerializeField] private GameObject bananaObject;
    [SerializeField] private int healAmount;

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
        mouseControllerRef.character.Health += healAmount;
        Debug.Log(mouseControllerRef.character.Health);
        currentCooldown = cooldownTurns;
        DeselectAbility();
    }

    public override void SelectAbility()
    {
        ExecuteAbility();
    }
}
