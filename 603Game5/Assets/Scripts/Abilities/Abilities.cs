using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    enum AbilityType
    {
        Self,
        Environment,
        Offensive,
    }

    [SerializeField] private string abilityName;
    [SerializeField] private string description;
    [SerializeField] private string cost;
    [SerializeField] private AbilityType type;

    private bool showTiles;

    void Start()
    {
        showTiles = false;
    }

    void Update()
    {

    }

    private void ExecuteAbility(int rangeModifier = 0)
    {

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
