using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointSorter : MonoBehaviour
{
    public void Update()
    {
        Waypoint[] grid = FindObjectsOfType<Waypoint>();
        grid = grid.OrderBy(go => go.name).ToArray();
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i].transform.SetSiblingIndex(i);
        }
    }
}
