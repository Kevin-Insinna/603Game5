using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public bool cameraLocked = true;
    public float cameraFov = 60.0f;
    public PauseScreenBehavior pauseScript;

    private float minFov = 30.0f;
    private float maxFov = 120.0f;

    void Update()
    {
        if (!pauseScript.isPaused)
        {
            if (Input.GetKey(KeyCode.W))
            {
                cameraLocked = false;
                transform.position += Vector3.right * Time.deltaTime * speed;
            }

            else if(Input.GetKey(KeyCode.S))
            {
                cameraLocked = false;
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
            
            else if(Input.GetKey(KeyCode.A))
            {
                cameraLocked = false;
                transform.position += Vector3.forward * Time.deltaTime * speed;
            }

            else if(Input.GetKey(KeyCode.D))
            {
                cameraLocked = false;
                transform.position += Vector3.back * Time.deltaTime * speed;
            }

            else if (Input.mouseScrollDelta.y != 0)
            {
                cameraLocked = false;
            }

            else if (Input.GetKey(KeyCode.R))
            {
                ResetCamera();
            }

            cameraFov -= Input.mouseScrollDelta.y * speed;
            cameraFov = Math.Clamp (cameraFov, minFov, maxFov);
            Camera.main.fieldOfView = cameraFov;
        }   
    }

    public void ResetCamera()
    {
        cameraLocked = true;
        cameraFov = 60;
    }
}
