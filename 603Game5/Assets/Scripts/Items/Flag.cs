using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Vector3 startingPosition;
    public GameObject winScreen;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detected");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attached to player");
            HoldFlag(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            DropFlag();
        }
        if (other.gameObject.CompareTag("Base"))
        {
            CaptureFlag();
        }
    }

    private void HoldFlag(GameObject parent)
    {
        transform.SetParent(parent.transform);
        transform.localPosition = new Vector3(0, 0, 0.55f);
    }

    private void DropFlag()
    {
        transform.SetParent(null);
        transform.position = startingPosition; 
    }

    private void CaptureFlag()
    {
        //Win state!
        winScreen.SetActive(true);
    }
}
