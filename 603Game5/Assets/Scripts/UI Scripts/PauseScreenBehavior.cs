using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseScreenBehavior : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameUI;

    //Bool to keep track of whether game is paused or not
    public bool isPaused;

    //Pause game when escape is pressed
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    //Toggles pause menu
    public void TogglePause()
    {
        isPaused = !isPaused;
        EventSystem.current.SetSelectedGameObject(null);

        pausePanel.SetActive(!pausePanel.activeInHierarchy);
        gameUI.SetActive(!gameUI.activeInHierarchy);
    }

    //Button Event: Quits to the title screen
    public void QuitToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
