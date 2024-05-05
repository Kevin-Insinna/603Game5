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


    //Range for environment and offensive abilities
    [Header("Environment and Offensive")]
    [SerializeField] protected int range;

    //Offensive Abilities
    [Header("Offensive")]
    [SerializeField] protected int damage;

    //Private helpers
    protected bool showTiles;
    protected MouseController mouseControllerRef;

    //Data tracker
    private DataTracker dataTracker;

    void Start()
    {
        showTiles = false;
        currentCooldown = 0;

        dataTracker = FindObjectOfType<DataTracker>();
    }

    //Offensive abilities
    public virtual void ExecuteAbility(Enemy chosenEnemy)
    {
        //Deal damage
        chosenEnemy.TakeDamage(damage);
        currentCooldown = cooldownTurns;
        SaveData();
        DeselectAbility();
    }

    public virtual void ExecuteAbility(GameObject chosenTile, int rangeModifier = 0)
    {
        SaveData();
        DeselectAbility();
    }

    public virtual void ExecuteAbility()
    {
        SaveData();
        DeselectAbility();
    }

    public abstract void SelectAbility();

    //Deselect ability after using it
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

    public void SaveData()
    {
        mouseControllerRef = MouseController.Instance;
        if (mouseControllerRef.character.playerInt  == 1)
        {
            dataTracker.ModifyP1Abilities(this);
        }
        else if (mouseControllerRef.character.playerInt == 2)
        {
            dataTracker.ModifyP2Abilities(this);
        }
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
