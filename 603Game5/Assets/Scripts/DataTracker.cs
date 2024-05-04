using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataTracker : MonoBehaviour
{
    //Data to save
    private System.DateTime startTime;
    public bool completedGame;

    //General Info
    [SerializeField] private bool playerWon { get; set; }
    [SerializeField] private int playerTurns { get; set; }

    //Player 1 Info

    //Abilities
    [SerializeField] private List<GameObject> player1Abilities = new List<GameObject>();
    [SerializeField] private int p1A1Used;
    public void ModifyP1A1() { p1A1Used++; }
    [SerializeField] private int p1A2Used;
    public void ModifyP1A2() { p1A2Used++; }
    [SerializeField] private int p1A3Used;
    public void ModifyP1A3() { p1A3Used++; }

    //General Info
    [SerializeField] private int p1SpacesMoved;
    public void ModifyP1Spaces() { p1SpacesMoved++; }

    //Player 2 Info

    //Abilities
    [SerializeField] private List<GameObject> player2Abilities = new List<GameObject>();
    [SerializeField] private int p2A1Used;
    public void ModifyP2A1() { p2A1Used++; }
    [SerializeField] private int p2A2Used;
    public void ModifyP2A2() { p2A2Used++; }
    [SerializeField] private int p2A3Used;
    public void ModifyP2A3() { p2A3Used++; }

    //General Info
    [SerializeField] private int p2SpacesMoved;
    public void ModifyP2Spaces() { p2SpacesMoved++; }


    public void Start()
    {
        SetStartTime();
        completedGame = false;
    }

    public void Save()
    {
        /* //Debug.Log("Negative choices: " + negativeChoices);
         string sceneName = SceneManager.GetActiveScene().name;
         string path = Directory.GetCurrentDirectory() + "/Data/" + sceneName + System.DateTime.Now.Month + "_" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "_" + System.DateTime.Now.Minute + ".txt";



         if (!File.Exists(path))
         {
             //Dialogue Setup
             string startTimeContent = "Start Time: " + startTime + "\n";
             string completedGameContent = "Completed Game?: " + completedGame + "\n";
             string finalSanityContent = "Final Sanity: " + finalSanity + "\n";

             string conversationCount = "Conversation Count Totals:\n" +
                 "Dr. Culver: " + culverDisCount + "\n" +
                 "Jennie: " + jennieDisCount + "\n" +
                 "Lucy: " + lucyDisCount + "\n" +
                 "Mary: " + maryDisCount + "\n";

             string choiceCount = "Conversation Type Selection Totals:\n" +
                 "Positive Choices: " + positiveChoices + "\n" +
                 "Neutral Choices: " + neutralChoices + "\n" +
                 "Negative Choices: " + negativeChoices + "\n";

             string endTime = "End Time: " + System.DateTime.Now;

             File.WriteAllText(path, startTimeContent);
             File.AppendAllText(path, completedGameContent);
             File.AppendAllText(path, finalSanityContent);
             File.AppendAllText(path, conversationCount);
             File.AppendAllText(path, choiceCount);
             File.AppendAllText(path, endTime);
         }*/
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
