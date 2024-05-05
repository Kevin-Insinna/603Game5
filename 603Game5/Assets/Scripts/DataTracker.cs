using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataTracker : MonoBehaviour
{
    //Data to save
    private System.DateTime startTime;

    //General Info  ----------
    [SerializeField] private bool playerWon;
    public void PlayerWon(bool won) { playerWon = won; }

    [SerializeField] private int playerTurns;
    public void AddPlayerTurns() { playerTurns++; }

    //Player 1 Info ----------

    //Abilities
    [SerializeField] private Dictionary<Abilities, int> player1Abilities = new Dictionary<Abilities, int>();
    public void AddP1Abilities(Abilities modifiedAbility)
    {
        player1Abilities.Add(modifiedAbility, 0);
    }
    public void ModifyP1Abilities(Abilities modifiedAbility)
    {
        if(player1Abilities.ContainsKey(modifiedAbility))
        {
            player1Abilities[modifiedAbility]++;
        }
    }

/*    [SerializeField] private int p1A1Used;
    public void ModifyP1A1() { p1A1Used++; }
    [SerializeField] private int p1A2Used;
    public void ModifyP1A2() { p1A2Used++; }
    [SerializeField] private int p1A3Used;
    public void ModifyP1A3() { p1A3Used++; }*/

    //General Info
    [SerializeField] private int p1SpacesMoved;
    public void ModifyP1Spaces() { p1SpacesMoved++; }

    [SerializeField] private int p1TimesTagged;
    public void ModifyP1Tagged() { p1TimesTagged++; }

    //Player 2 Info ----------

    //Abilities
    [SerializeField] private Dictionary<Abilities, int> player2Abilities = new Dictionary<Abilities, int>();
    public void AddP2Abilities(Abilities modifiedAbility)
    {
        player2Abilities.Add(modifiedAbility, 0);
    }
    public void ModifyP2Abilities(Abilities modifiedAbility)
    {
        if (player2Abilities.ContainsKey(modifiedAbility))
        {
            player2Abilities[modifiedAbility]++;
        }
    }

    //General Info
    [SerializeField] private int p2SpacesMoved;
    public void ModifyP2Spaces() { p2SpacesMoved++; }

    [SerializeField] private int p2TimesTagged;
    public void ModifyP2Tagged() { p2TimesTagged++; }

    //Enemy Data
    [SerializeField] private int enemyDeaths;
    public void ModifyEnemyDeaths() { enemyDeaths++; }

    public void Start()
    {
        SetStartTime();
        playerWon = false;
    }

    public void Save()
    {
        //Debug.Log("Negative choices: " + negativeChoices);
        string sceneName = SceneManager.GetActiveScene().name;
        string path = Directory.GetCurrentDirectory() + "/Data/" + sceneName + "_" + System.DateTime.Now.Month + "_" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "_" + System.DateTime.Now.Minute + ".txt";

        if (!File.Exists(path))
        {
            Debug.Log("Saving Data");

            //Basic Info
            string startTimeContent = "Start Time: " + startTime + "\n";
            string endTime = "End Time: " + System.DateTime.Now + "\n";
            string completedGameContent = "Won Game?: " + playerWon + "\n";
            string finalPlayerTurns = "Total Player Turns: " + playerTurns + "\n\n";

            //Player 1
            string player1AbilitiesContent = "Player 1 Abilities:\n";
            foreach(KeyValuePair<Abilities, int> a in player1Abilities)
            {
                player1AbilitiesContent += a.Key.abilityName + "\n";
                player1AbilitiesContent += "Times Used: " + a.Value + "\n";
            }

            player1AbilitiesContent += "\nSpaces Moved: " + p1SpacesMoved + "\n";
            player1AbilitiesContent += "Times Tagged: " + p1TimesTagged + "\n\n";

            //Player 2
            string player2AbilitiesContent = "Player 2 Abilities:\n";
            foreach (KeyValuePair<Abilities, int> a in player2Abilities)
            {
                player2AbilitiesContent += a.Key.abilityName + "\n";
                player2AbilitiesContent += "Times Used: " + a.Value + "\n";
            }

            player2AbilitiesContent += "\nSpaces Moved: " + p2SpacesMoved + "\n";
            player2AbilitiesContent += "Times Tagged: " + p2TimesTagged + "\n\n";

            string enemyContent = "Enemy Info\n"
                + "Enemy Deaths: " + enemyDeaths;


            File.WriteAllText(path, startTimeContent);
            File.AppendAllText(path, endTime);
            File.AppendAllText(path, completedGameContent);
            File.AppendAllText(path, finalPlayerTurns);
            File.AppendAllText(path, player1AbilitiesContent);
            File.AppendAllText(path, player2AbilitiesContent);
            File.AppendAllText(path, enemyContent);

            
        }
    }

    /*public void SaveAndQuit()
    {
        StartCoroutine(SaveAndQuitTimed());
    }*/

    /*private IEnumerator SaveAndQuitTimed()
    {
        Save();
        yield return new WaitForSeconds(5f);
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }*/

    public void SetStartTime()
    {
        startTime = System.DateTime.Now;
    }
}
