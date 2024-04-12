using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<PlayerCharacter> characterList;
    public MouseController cursorScript;

    int characterListIndex = 0;
    PlayerCharacter currentlyActiveCharacter;

    Vector3 cameraDistance = new Vector3(5.363f, 5.874f, -3.972f);

    public void Start()
    {
        foreach(PlayerCharacter p in characterList)
        {
            p.IsActiveTurn = false;
        }

        currentlyActiveCharacter = characterList[characterListIndex];   
        currentlyActiveCharacter.IsActiveTurn = true;
        cursorScript.character = currentlyActiveCharacter;
    }

    public void Update()
    {
        transform.position = currentlyActiveCharacter.gameObject.transform.position + cameraDistance;
    }

    public void EndTurn()
    {
        Debug.Log("Button pressed");
        if (characterListIndex == characterList.Count - 1) 
        {
            characterListIndex = 0;
        }

        else
        {
            characterListIndex++;
        }
        currentlyActiveCharacter = characterList[characterListIndex];
        currentlyActiveCharacter.IsActiveTurn = true;
        cursorScript.character = currentlyActiveCharacter;
    }
}
