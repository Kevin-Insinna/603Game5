using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnboardingBehavior : MonoBehaviour
{
    public PauseScreenBehavior pauseScript;

    private int onboardingState = 1;

    void Start()
    {
        pauseScript.isPaused = true;
    }

    public void ContinueOnboarding()
    {
        onboardingState++;

        switch (onboardingState)
        {

        }
    }
}
