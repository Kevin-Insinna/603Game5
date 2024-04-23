using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, List<OverlayTile> searchableTiles, bool isPlayer)
    {
        Debug.Log("Start location: " + start.gridLocation);
        Debug.Log("End location: " + end.gridLocation);

        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);

        int iterationCount = 0;

        while(openList.Count > 0)
        {
            iterationCount++;
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            Debug.Log(currentOverlayTile.gridLocation);

            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);

            if(currentOverlayTile == end)
            {
                Debug.Log("Found path");
                return GetFinishedList(start, end);
            }

            var neighborTiles = MapManager.Instance.GetNeighborTiles(currentOverlayTile, searchableTiles);

            foreach (var neighborTile in neighborTiles)
            {
                //1 is jump height
                if (isPlayer)
                {
                    if (neighborTile.isBlocked || closedList.Contains(neighborTile))
                    {
                        continue;
                    }
                }
                else
                {
                    if (closedList.Contains(neighborTile))
                    {
                        continue;
                    }
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
        Debug.Log("Iteration count: " + iterationCount);
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

}
