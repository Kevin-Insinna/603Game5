using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Vector3 worldPosition;
    public LayerMask layersToHit;
    private bool tileIsHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //var focusedTileHit = GetFocusedOnTile();

        /*if (focusedTileHit.HasValue)
        {         
            //GameObject overlayTile = focusedTileHit.Value.collider.gameObject;
            //transform.position = overlayTile.transform.position;
            //gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
        }*/

        var focusedTileHit = GetFocusedOnTile();

        if (tileIsHit)
        {
            //transform.position = worldPosition;

            GameObject overlayTile = focusedTileHit.Value.collider.gameObject;
            transform.position = overlayTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            if (Input.GetMouseButtonDown(0))
            {
                overlayTile.GetComponent<OverlayTile>().ShowTile();
            }
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

/*        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPosition);

        Debug.Log(mousePos);

        RaycastHit[] hits = Physics.RaycastAll(mousePos, Vector3.zero);

        if(hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;*/
    }

}
