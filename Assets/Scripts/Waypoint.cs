using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public const int gridSize = 10;
    public bool isExplored = false;
    public Waypoint exploredFrom;


    Vector2Int waypointPosition;

    public Vector2Int GetWaypointPosition()
    {
        waypointPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
        return waypointPosition;
    }

    public void SetColor(Color color)
    {
        MeshRenderer top = transform.Find("Top").GetComponent<MeshRenderer>();
        Material material = new Material(top.sharedMaterial);
        material.color = color;
        top.sharedMaterial = material;
    }
}