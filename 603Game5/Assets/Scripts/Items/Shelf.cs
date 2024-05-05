using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shelf : MonoBehaviour
{
    [SerializeField]
    private Vector3Int tileOne;
    [SerializeField]
    private Vector3Int tileTwo;

    [SerializeField]
    private Tilemap tMap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockShelf()
    {
        MapManager mmgr = MapManager.Instance;

        //Weird rounding fix for offset grid
        int xValueOne;
        int zValueOne;
        int xValueTwo;
        int zValueTwo;

        //Adjust x value
        if (tileOne.x >= 0)
            xValueOne = -(int)(-tileOne.x);
        else if (tileOne.x == -0.5)
            xValueOne = -1;
        else
            xValueOne = -Mathf.CeilToInt(Mathf.Abs(tileOne.x));

        //Adjust z value
        if (tileOne.z >= 0)
            zValueOne = -(int)(-tileOne.z);
        else if (tileOne.z == -0.5)
            zValueOne = -1;
        else
            zValueOne = -Mathf.CeilToInt(Mathf.Abs(tileOne.z));
        /*
                Debug.Log("Char Position: " + character.transform.position.x + "Xvalue:" + xValue);
                Debug.Log("Char Position: " + character.transform.position.z + "Zvalue:" + zValue);*/

        //Adjust x value
        if (tileTwo.x >= 0)
            xValueTwo = -(int)(-tileTwo.x);
        else if (tileTwo.x == -0.5)
            xValueTwo = -1;
        else
            xValueTwo = -Mathf.CeilToInt(Mathf.Abs(tileTwo.x));

        //Adjust z value
        if (tileTwo.z >= 0)
            zValueTwo = -(int)(-tileTwo.z);
        else if (tileTwo.z == -0.5)
            zValueTwo = -1;
        else
            zValueTwo = -Mathf.CeilToInt(Mathf.Abs(tileTwo.z));

        Vector2Int locationOneToCheck = new Vector2Int(xValueOne, zValueOne);
        Vector2Int locationTwoToCheck = new Vector2Int(xValueTwo, zValueTwo);

        if (mmgr.map.ContainsKey(locationOneToCheck))
        {
            //mmgr.map[locationOneToCheck].isBlocked = true;
            mmgr.map.Remove(locationOneToCheck);
        }
        if (mmgr.map.ContainsKey(locationTwoToCheck))
        {
            //mmgr.map[locationTwoToCheck].isBlocked = true;
            mmgr.map.Remove(locationTwoToCheck);
        }

        //tMap.SetTile(tileOne, null);
        ////mmgr.map.Remove(new Vector2Int(tileOne.x, tileOne.z));
        //mmgr.map[new Vector2Int(tileOne.x, tileOne.z)].isBlocked = true;
        //tMap.SetTile(tileTwo, null);
        ////mmgr.map.Remove(new Vector2Int(tileTwo.x, tileTwo.z));
        //mmgr.map[new Vector2Int(tileOne.x, tileOne.z)].isBlocked = true;
        this.gameObject.transform.Rotate(new Vector3(-70f, 0f, 0f));
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 0.7f);
    }
}
