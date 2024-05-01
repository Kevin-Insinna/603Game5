using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bars : MonoBehaviour
{
    [SerializeField]
    private BoxCollider entranceOne;
    [SerializeField]
    private BoxCollider entranceTwo;

    [SerializeField]
    private Vector3 tileOne;
    [SerializeField]
    private Vector3 tileTwo;

    private bool xDir;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 diffTile = tileOne - tileTwo;
        if(diffTile.x == 0)
        {
            xDir = false;
        }
        else
        {
            xDir = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 CrossMonkeyBars(Vector3 playerPos)
    {
        //if (xDir)
        //{
            if (/*entranceOne.center.x - playerPos.x < entranceTwo.center.x - playerPos.x*/playerPos == tileOne)
            {
                return tileTwo;
            }
            else
            {
                return tileOne;
            }
        //}
        //else
        //{
        //    if (entranceOne.center.z - playerPos.z < entranceTwo.center.z - playerPos.z)
        //    {
        //        return tileOne;
        //    }
        //    else
        //    {
        //        return tileTwo;
        //    }
        //}
        
    }
}
