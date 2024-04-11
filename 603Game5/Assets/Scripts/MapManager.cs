using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    public TileBase tilePrefab;

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

        Debug.Log(tileMap);

        BoundsInt bounds = tileMap.cellBounds;

        Debug.Log(bounds);

        Debug.Log(bounds.min.y);

        for (int z = bounds.min.z; z <= bounds.max.z; z++)
        {
            for (int y = bounds.max.y; y >= bounds.min.y; y--)
            {
                for (int x = bounds.min.x; x <= bounds.max.x; x++)
                {
                    var tileLocation = new Vector3Int(x, y, z);             

                    if (tileMap.HasTile(tileLocation))
                    {
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y + 0.51f, cellWorldPosition.z);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
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
