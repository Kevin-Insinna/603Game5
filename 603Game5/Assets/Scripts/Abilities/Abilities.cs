using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Abilities : MonoBehaviour
{
    public enum AbilityType
    {
        Self,
        Environment,
        Offensive,
    }

    [SerializeField] public string abilityName;
    [SerializeField] public string description;
    //[SerializeField] protected int cost;
    [SerializeField] public AbilityType type;
    [SerializeField] public int cooldownTurns;
    [SerializeField] public int currentCooldown;

    private bool showTiles;

    public MouseController mouseControllerRef;

    void Start()
    {
        showTiles = false;
        currentCooldown = 0;
    }

    void Update()
    {

    }


    public abstract void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0);

    public abstract void ExecuteAbility();

    public abstract void SelectAbility();

    public virtual void DeselectAbility()
    {
        if (mouseControllerRef.character.MovementLeft > 0)
        {
            //Debug.Log("This is running");
            mouseControllerRef.character.CanMove = true;
        }

        mouseControllerRef.HideCurrentTiles();
        mouseControllerRef.GetInRangeTiles(mouseControllerRef.character.MovementLeft);
        mouseControllerRef.character.selectedAbility = null;
    }

    public void ToggleRangeHighlight(int rangeModifier = 0)
    {
        if (showTiles)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            showTiles = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            showTiles = true;
        }
    }


}
