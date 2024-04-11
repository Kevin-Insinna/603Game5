using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var tileMap = gameObject.GetComponentInChildren<Tilemap>();

        //Debug.Log(tileMap);

        BoundsInt bounds = tileMap.cellBounds;

        Debug.Log(bounds);

        for(int y = 2; y > 0; y--)
        {
            for (int x = -5; y < 5; x++)
            {
                for (int z = -5; z < 5; z++)
                {
                    var tileLocation = new Vector3Int(x, y, z);

                    var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                    var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;

                    Debug.Log("This is running");

                    if (tileMap.HasTile(tileLocation))
                    {
                     
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
