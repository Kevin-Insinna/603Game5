using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnboardingBehavior : MonoBehaviour
{
    public PauseScreenBehavior pauseScript;
    public GameObject cameraFocus;
    public GameObject gameUI;
    public TextMeshProUGUI onboardingText;
    public TextMeshProUGUI titleText;

    private int onboardingState = 0;
    Vector3 cameraDistance = new Vector3(-4.17f, 4.75f, -0.07f);

    void Start()
    {
        pauseScript.isPaused = true;
        gameUI.SetActive(false);
    }

    public void ContinueOnboarding()
    {
        onboardingState++;

        switch (onboardingState)
        {
            case 1:
                this.GetComponent<RectTransform>().localPosition = new Vector3(0, -245, 0);
                titleText.text = "THE FLAG";
                onboardingText.text = "This is the flag. \n\nYour goal is to capture it and bring it back to your base";
                cameraFocus = GameObject.Find("Flag");
                break;
            case 2:
                titleText.text = "THE BASE";
                onboardingText.text = "This is your base. \n\nGet one of your characters here with the flag to win!";
                cameraFocus = GameObject.Find("Sandbox_Base");
                break;
            case 3:
                this.GetComponent<RectTransform>().localPosition = Vector3.zero;
                gameUI.SetActive(true);
                titleText.text = "ABILITIES";
                onboardingText.text = "Hovering over your abilities will tell you what they do. \n\nUse them to defeat your opponents and capture their flag!";
                break;
            case 4:
                titleText.text = "MOVEMENT";
                onboardingText.text = "You can move the active character by clicking an available square. \n\nEach character can move once per turn";
                break;
            case 5:
                titleText.text = "CAMERA CONTROLS";
                onboardingText.text = "Move - WASD \nZoom - Scroll Wheel. \nReset - R \n\nControls can be viewed again in the pause menu.";
                break;
            case 6:
                this.gameObject.SetActive(false);
                pauseScript.isPaused = false;
                break;
        }
   
        Camera.main.gameObject.transform.position = cameraFocus.gameObject.transform.position + cameraDistance;
    }
}
