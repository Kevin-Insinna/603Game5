using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<GameObject> characterList;

    int characterListIndex = 0;
    GameObject currentlyActiveCharacter;

    public void Start()
    {
        currentlyActiveCharacter = characterList[characterListIndex];   
    }

    public void Update()
    {
        transform.position = currentlyActiveCharacter.transform.position + new Vector3(5.363f, 5.874f, -3.972f);
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
    }
}
