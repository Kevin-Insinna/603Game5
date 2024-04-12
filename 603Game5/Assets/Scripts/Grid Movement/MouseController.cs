using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class MouseController : MonoBehaviour
{
    public Vector3 worldPosition;
    public LayerMask layersToHit;
    private bool tileIsHit;

    public GameObject characterPrefab;
    private PlayerCharacter character;

    private PathFinder pathFinder;
    private List<OverlayTile> path = new List<OverlayTile>();

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        character = characterPrefab.GetComponent<PlayerCharacter>();
        pathFinder = new PathFinder();

/*
        tileMap.HasTile(tileLocation)
        character.activeTile = */
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusedTileHit = GetFocusedOnTile();

        if (tileIsHit)
        {
            GameObject overlayTile = focusedTileHit.Value.collider.gameObject;
            transform.position = overlayTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            if (Input.GetMouseButtonDown(0))
            {
                overlayTile.GetComponent<OverlayTile>().ShowTile();

                path = pathFinder.FindPath(character.activeTile, overlayTile.GetComponent<OverlayTile>());
            }
        }

        if(path.Count > 0)
        {
            MoveAlongPath();
        }
        //Scuffed fix to current active tile hovering above group
        else
        {
            character.activeTile.transform.position = new Vector3(character.activeTile.transform.position.x, 0.51f, character.activeTile.transform.position.z);
        }
    }

    private void MoveAlongPath()
    {
        //Debug.Log(path.Count);
        var step = speed * Time.deltaTime;

        character.transform.position = Vector3.MoveTowards(character.transform.position, path[0].transform.position, step);

        //Vector3 tempPathLocation = new Vector3(path[0].transform.position.x, 0.82f, path[0].transform.position.z);

        character.transform.position = new Vector3(character.transform.position.x, 0.82f, character.transform.position.z);
        path[0].transform.position = new Vector3(path[0].transform.position.x, 0.82f, path[0].transform.position.z);

        if(Vector3.Distance(character.transform.position, path[0].transform.position) < .01f)
        {
            PositionCharacterOnMap(path[0]);
            path.RemoveAt(0);
        }
    }

    public RaycastHit? GetFocusedOnTile()
    {
        Vector3 screenPosition = Input.mousePosition;
        //screenPosition.z = Camera.main.nearClipPlane + 1;
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            worldPosition = hit.point;
            tileIsHit = true;

            return hit;
        }
        else
        {
            tileIsHit = false;
            return null;
        }
    }

    private void PositionCharacterOnMap(OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        character.activeTile = tile;
    }
}
