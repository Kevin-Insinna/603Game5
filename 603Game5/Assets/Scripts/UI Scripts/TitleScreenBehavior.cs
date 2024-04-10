using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenBehavior : MonoBehaviour
{
    public GameObject optionsPanel;

    //Button Event: Loads GameScene
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    //Button Event: Toggles options menu
    public void ToggleOptions()
    {
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
    }


    //Button Event: Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
