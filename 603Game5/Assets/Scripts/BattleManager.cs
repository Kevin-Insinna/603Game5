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
    private DataTracker dataTracker;

    int characterListIndex = 0;
    PlayerCharacter currentlyActiveCharacter;

    int enemyListIndex = 0;
    Enemy currentlyActiveEnemy;

    private bool isPlayerTurn;

    Vector3 cameraDistance = new Vector3(-3.17f, 4.75f, -0.07f);

    [SerializeField] private GameObject endTurnButton;

    //Setting up battle manage instance
    private static BattleManager _instance;
    public static BattleManager Instance { get { return _instance; } }

    public void Awake()
    {
        //Make instance of this
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //Add all players and set currently active player
        foreach (PlayerCharacter p in characterList)
        {
            p.IsActiveTurn = false;
            p.MovementLeft = p.TileRange;
        }

        currentlyActiveCharacter = characterList[characterListIndex];   
        currentlyActiveCharacter.IsActiveTurn = true;
        cursorScript.character = currentlyActiveCharacter;

        //Add all enemies and set currently active enemy
        foreach (Enemy p in enemyList)
        {
            p.isActiveTurn = false;
        }

        currentlyActiveEnemy = enemyList[enemyListIndex];
        currentlyActiveEnemy.isActiveTurn = true;
        //cursorScript.character = currentlyActiveEnemy;

        isPlayerTurn = true;

        //Setup Data Tracker
        dataTracker = FindObjectOfType<DataTracker>();

        //Add abilities to dictionary 
        foreach(Abilities a in characterList[0].abilityList)
        {
            dataTracker.AddP1Abilities(a);
        }
        foreach (Abilities a in characterList[1].abilityList)
        {
            dataTracker.AddP2Abilities(a);
        }

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
            }
        }
        else
        {
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

                enemyListIndex++;
            }
        }

        this.gameObject.GetComponent<CameraMovement>().ResetCamera();
    }

    public void SwapTeam()
    {
        //Debug.Log("Swapping teams");
        if (isPlayerTurn)
        {
            dataTracker.AddPlayerTurns();
            isPlayerTurn = false;
            endTurnButton.SetActive(false);
        }          
        else
        {
            isPlayerTurn = true;
            endTurnButton.SetActive(true);

            ResetPlayer();
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
