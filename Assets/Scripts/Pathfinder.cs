using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
        {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
        };
    
    Waypoint currentWaypoint;

    bool isPathfinding = true;
    private void Pathfind()
    {
        StoreWaypoints();
        ColorStartAndEndWaypoints();
        BreadthFirstSearch();
        CreatePath();
    }

    private void StoreWaypoints()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            Vector2Int waypointPosition = waypoint.GetWaypointPosition();

            if (grid.ContainsKey(waypointPosition))
            {
                Debug.LogWarning("Skipping Overlapping Waypoint: " + waypoint.name);
            }
            else 
            {
                grid.Add(waypointPosition, waypoint);
            }
        }
    }

    private void ColorStartAndEndWaypoints()
    {
        startWaypoint.SetColor(Color.green);
        endWaypoint.SetColor(Color.red);
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isPathfinding)
        {
            currentWaypoint = queue.Dequeue();
            currentWaypoint.isExplored = true;

            if(currentWaypoint == endWaypoint)
            {
                isPathfinding = false;
            }
            else 
            {
                ExploreNextWaypoints();
            }
        }
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);
        Waypoint previousWaypoint = endWaypoint.exploredFrom;
        while (previousWaypoint != startWaypoint)
        {
            path.Add(previousWaypoint);
            previousWaypoint = previousWaypoint.exploredFrom;
        }
        path.Add(startWaypoint);

        path.Reverse();
    }

    private void ExploreNextWaypoints()
    {
        if (!isPathfinding) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinates = currentWaypoint.GetWaypointPosition() + direction;
            if (grid.ContainsKey(explorationCoordinates))
            {
                QueueNextWaypoints(explorationCoordinates);
            }
        }
    }

    private void QueueNextWaypoints(Vector2Int explorationCoordinates)
    {
        Waypoint newWaypoint = grid[explorationCoordinates];
        if (newWaypoint.isExplored || queue.Contains(newWaypoint))
        {
            ;
        }
        else 
        {
            queue.Enqueue(newWaypoint);
            newWaypoint.exploredFrom = currentWaypoint;
        }
    }

    public Waypoint getStartWaypoint()
    {
        return startWaypoint;
    }

    public Waypoint getEndWaypoint()
    {
        return endWaypoint;
    }

    public List<Waypoint> getPath()
    {
        Pathfind();
        return path;
    }
}
