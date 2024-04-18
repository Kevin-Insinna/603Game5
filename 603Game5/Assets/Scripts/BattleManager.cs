using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<PlayerCharacter> characterList;
    public List<Enemy> enemyList;
    public MouseController cursorScript;

    int characterListIndex = 0;
    PlayerCharacter currentlyActiveCharacter;

    int enemyListIndex = 0;
    Enemy currentlyActiveEnemy;

    private bool isPlayerTurn;

    Vector3 cameraDistance = new Vector3(-3.17f, 4.75f, -0.07f);

    [SerializeField] private GameObject endTurnButton;

    public void Awake()
    {
        foreach(PlayerCharacter p in characterList)
        {
            p.IsActiveTurn = false;
        }

        currentlyActiveCharacter = characterList[characterListIndex];   
        currentlyActiveCharacter.IsActiveTurn = true;
        cursorScript.character = currentlyActiveCharacter;


        foreach (Enemy p in enemyList)
        {
            p.isActiveTurn = false;
        }

        currentlyActiveEnemy = enemyList[enemyListIndex];
        currentlyActiveEnemy.isActiveTurn = true;
        //cursorScript.character = currentlyActiveEnemy;

        isPlayerTurn = true;
    }

    public void Update()
    {
        if (isPlayerTurn)
        {
            transform.position = currentlyActiveCharacter.gameObject.transform.position + cameraDistance;
        }
        else
        {
            transform.position = currentlyActiveEnemy.gameObject.transform.position + cameraDistance;
        }

    }

    public void EndTurn()
    {
        Debug.Log("End turn is running");
        if (isPlayerTurn)
        {
            cursorScript.HideCurrentTiles();

            Debug.Log(characterListIndex);
            //Debug.Log("Button pressed");
            if (characterListIndex == characterList.Count - 1)
            {
                characterListIndex = 0;
                SwapTeam();
                EndTurn();
            }
            else
            {
                characterListIndex++;

                currentlyActiveCharacter = characterList[characterListIndex];
                currentlyActiveCharacter.IsActiveTurn = true;
                currentlyActiveCharacter.CanMove = true;
                cursorScript.character = currentlyActiveCharacter;

                cursorScript.GetInRangeTiles();
            }
        }
        else
        {
            Debug.Log("Enemy list count " + enemyList.Count);
            Debug.Log("Enemy list index " + enemyListIndex);
            if (enemyListIndex > enemyList.Count - 1)
            {
                enemyListIndex = 0;
                SwapTeam();
            }
            else
            {
                currentlyActiveEnemy = enemyList[enemyListIndex];
                currentlyActiveEnemy.isActiveTurn = true;
                currentlyActiveEnemy.canMove = true;
                //cursorScript.character = currentlyActiveCharacter;
                //cursorScript.GetInRangeTiles();

                enemyListIndex++;
            }
        }

    }

    public void SwapTeam()
    {
        if (isPlayerTurn)
        {
            isPlayerTurn = false;
            endTurnButton.SetActive(false);
        }          
        else
        {
            isPlayerTurn = true;
            endTurnButton.SetActive(true);

            currentlyActiveCharacter = characterList[characterListIndex];
            currentlyActiveCharacter.IsActiveTurn = true;
            currentlyActiveCharacter.CanMove = true;
            cursorScript.character = currentlyActiveCharacter;

            cursorScript.GetInRangeTiles();
        }
        
    }
}
