using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverworldBehavior : MonoBehaviour
{
    static public bool clearedLevelOne;

    [SerializeField]
    private Button levelTwoBtn;

    // Start is called before the first frame update
    void Start()
    {
        clearedLevelOne = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(string levelName)
    {
        //if (clearedLevelOne)
        //{
            SceneManager.LoadScene(levelName);
        //}
        //else if(levelName == "LevelTest2")
        //{
        //    clearedLevelOne = true;
        //    SceneManager.LoadScene(levelName);
        //}
    }
}
