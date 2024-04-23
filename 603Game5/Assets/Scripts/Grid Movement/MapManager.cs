using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    public List<GameObject> characters;

    //public TileBase tilePrefab;

    public Dictionary<Vector2Int, OverlayTile> map;


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

        var tileMap = gameObject.GetComponentInChildren<Tilemap>();
        map = new Dictionary<Vector2Int, OverlayTile>();

        BoundsInt bounds = tileMap.cellBounds;

        for (int z = bounds.min.z; z <= bounds.max.z; z++)
        {
            for (int y = bounds.min.y; y <= bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x <= bounds.max.x; x++)
                {
                    Vector3Int tileLocation = new Vector3Int(x, y, z);
                    Vector2Int tileKey = new Vector2Int(x, y);

                    if (tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                    {
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y + 0.51f, cellWorldPosition.z);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                        overlayTile.gridLocation = tileLocation;

                        if (tileLocation == new Vector3Int(0, 0, y))
                        {
                            foreach (GameObject c in characters)
                            {
                                c.GetComponent<PlayerCharacter>().activeTile = overlayTile;
                            }
                        }

                        map.Add(tileKey, overlayTile);
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {     
    }

    public List<OverlayTile> GetNeighborTiles(OverlayTile overlayTile, List<OverlayTile> searchableTiles)
    {
        Dictionary<Vector2Int, OverlayTile> tileToSearch = new Dictionary<Vector2Int, OverlayTile>();

        if(searchableTiles.Count > 0)
        {
            foreach(var item in searchableTiles)
            {
                tileToSearch.Add(item.Grid2DLocation, item);
            }
        }
        else
        {
            tileToSearch = map;
        }

        List<OverlayTile> neighbors = new List<OverlayTile>();

        //Top Neighbor
        Vector2Int locationToCheck = new Vector2Int(overlayTile.gridLocation.x, overlayTile.gridLocation.y + 1);
        if (tileToSearch.ContainsKey(locationToCheck))
        {
            //If y height is figured out if(Mathf.Abs(currentOverlayTile.gridLocation.y - map[locationToCheck].gridLocation.y > 1))
            neighbors.Add(map[locationToCheck]);
        }

        //Bottom Neighbor
        locationToCheck = new Vector2Int(overlayTile.gridLocation.x, overlayTile.gridLocation.y - 1);
        if (tileToSearch.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }

        //Right Neighbor
        locationToCheck = new Vector2Int(overlayTile.gridLocation.x + 1, overlayTile.gridLocation.y);
        if (tileToSearch.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }

        //Left Neighbor
        locationToCheck = new Vector2Int(overlayTile.gridLocation.x - 1, overlayTile.gridLocation.y);
        if (tileToSearch.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }

        return neighbors;
    }
}
