using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<GameObject> characterList;

    int characterListIndex = 0;
    GameObject currentlyActiveCharacter;

    Vector3 cameraDistance = new Vector3(5.363f, 5.874f, -3.972f);

    public void Start()
    {
        currentlyActiveCharacter = characterList[characterListIndex];   
    }

    public void Update()
    {
        transform.position = currentlyActiveCharacter.transform.position + cameraDistance;
    }

    public void EndTurn()
    {
        if (characterListIndex == characterList.Count - 1) 
        {
            characterListIndex = 0;
        }

        else
        {
            characterListIndex++;
        }
        currentlyActiveCharacter = characterList[characterListIndex];
        
        //Set conditions in currentlyActiveCharacter so they can perform their actions for the turn
    }
}
