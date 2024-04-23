using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<PlayerCharacter> characterList;
    public List<Enemy> enemyList;
    public MouseController cursorScript;
    public GameObject onboardingPanel;
    public TextMeshProUGUI movementText;

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
            p.MovementLeft = p.TileRange;
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
        if (this.gameObject.GetComponent<CameraMovement>().cameraLocked && !onboardingPanel.activeInHierarchy)
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

        if (currentlyActiveCharacter != null)
        {
            movementText.text = "Movement Points: " + currentlyActiveCharacter.MovementLeft.ToString();
        }
    }

    public void EndTurn()
    {
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
                ResetPlayer();
/*                //Set previous character to not running
                currentlyActiveCharacter.IsActiveTurn = false;
                cursorScript.AbilityUpdateTurn();

                //Update new character
                characterListIndex++;
                currentlyActiveCharacter = characterList[characterListIndex];

                //Reset character stats
                currentlyActiveCharacter.MovementLeft = currentlyActiveCharacter.TileRange;
                currentlyActiveCharacter.IsActiveTurn = true;
                currentlyActiveCharacter.CanMove = true;
                cursorScript.character = currentlyActiveCharacter;
                currentlyActiveCharacter.activeTile = cursorScript.GetActiveTile();
                cursorScript.UpdateButtons();

                //Show tiles
                cursorScript.GetInRangeTiles(currentlyActiveCharacter.MovementLeft);*/
            }
        }
        else
        {
/*            Debug.Log("Enemy list count " + enemyList.Count);
            Debug.Log("Enemy list index " + enemyListIndex);*/
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

        this.gameObject.GetComponent<CameraMovement>().ResetCamera();
    }

    public void SwapTeam()
    {
        Debug.Log("Swapping teams");
        if (isPlayerTurn)
        {
            isPlayerTurn = false;
            endTurnButton.SetActive(false);
        }          
        else
        {
            isPlayerTurn = true;
            endTurnButton.SetActive(true);

            ResetPlayer();

/*            currentlyActiveCharacter.IsActiveTurn = false;
            cursorScript.AbilityUpdateTurn();

            currentlyActiveCharacter = characterList[characterListIndex];

            currentlyActiveCharacter.MovementLeft = currentlyActiveCharacter.TileRange;
            currentlyActiveCharacter.IsActiveTurn = true;
            currentlyActiveCharacter.CanMove = true;
            cursorScript.character = currentlyActiveCharacter;
            currentlyActiveCharacter.activeTile = cursorScript.GetActiveTile();
            cursorScript.UpdateButtons();

            cursorScript.GetInRangeTiles(currentlyActiveCharacter.MovementLeft);*/
        }

        this.gameObject.GetComponent<CameraMovement>().ResetCamera();
    }

    public void ResetPlayer()
    {
        //Set previous character to not running
        currentlyActiveCharacter.IsActiveTurn = false;
        cursorScript.AbilityUpdateTurn();

        //Update new character
        currentlyActiveCharacter = characterList[characterListIndex];

        //Reset character stats
        currentlyActiveCharacter.MovementLeft = currentlyActiveCharacter.TileRange;
        currentlyActiveCharacter.IsActiveTurn = true;
        currentlyActiveCharacter.CanMove = true;
        cursorScript.character = currentlyActiveCharacter;
        currentlyActiveCharacter.activeTile = cursorScript.GetActiveTile();
        cursorScript.UpdateButtons();

        //Show tiles
        cursorScript.GetInRangeTiles(currentlyActiveCharacter.MovementLeft);
    }
}
