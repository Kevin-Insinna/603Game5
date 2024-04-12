using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end)
    {
        Debug.Log("Start location: " + start.gridLocation);
        Debug.Log("End location: " + end.gridLocation);

        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);

        while(openList.Count > 0)
        {
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);

            if(currentOverlayTile == end)
            {
                return GetFinishedList(start, end);
            }

            var neighborTiles = GetNeighborTiles(currentOverlayTile);

            foreach (var neighborTile in neighborTiles)
            {
                //1 is jump height
                if (neighborTile.isBlocked || closedList.Contains(neighborTile))//If y height is figured out || Mathf.Abs(currentOverlayTile.gridLocation.y - neighborTile.gridLocation.y > 1)
                {
                    continue;
                }

                neighborTile.G = GetManhattanDistance(start, neighborTile);
                neighborTile.H = GetManhattanDistance(end, neighborTile);

                neighborTile.previous = currentOverlayTile;

                if (!openList.Contains(neighborTile))
                {
                    openList.Add(neighborTile);
                }
            }
        }

        return new List<OverlayTile>();
    }

    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile>();
        OverlayTile currentTile = end;

        while(currentTile != start)
        {
            //Debug.Log("Cycling through list");
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhattanDistance(OverlayTile start, OverlayTile neighborTile)
    {
        return Mathf.Abs(start.gridLocation.x - neighborTile.gridLocation.x) + Mathf.Abs(start.gridLocation.z - neighborTile.gridLocation.z);
    }



    private List<OverlayTile> GetNeighborTiles(OverlayTile overlayTile)
    {
        var map = MapManager.Instance.map;

        List<OverlayTile> neighbors = new List<OverlayTile>();

        //Top Neighbor
        Vector2Int locationToCheck = new Vector2Int(overlayTile.gridLocation.x, overlayTile.gridLocation.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }

        //Bottom Neighbor
        locationToCheck = new Vector2Int(overlayTile.gridLocation.x, overlayTile.gridLocation.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }

        //Right Neighbor
        locationToCheck = new Vector2Int(overlayTile.gridLocation.x + 1, overlayTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }

        //Left Neighbor
        locationToCheck = new Vector2Int(overlayTile.gridLocation.x - 1, overlayTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighbors.Add(map[locationToCheck]);
        }

        return neighbors;
    }
}
