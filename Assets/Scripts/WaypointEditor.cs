using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class WaypointEditor : MonoBehaviour
{
    Waypoint waypoint;
    int gridSize = Waypoint.gridSize;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        transform.position = new Vector3(
            waypoint.GetWaypointPosition().x * gridSize, 0f, 
            waypoint.GetWaypointPosition().y * gridSize
            );
    }

    private void UpdateLabel()
    {
        TextMesh label = GetComponentInChildren<TextMesh>();

        string labelText = 
            waypoint.GetWaypointPosition().x + "," + 
            waypoint.GetWaypointPosition().y;

        label.text = labelText;
        gameObject.name = labelText;
    }
}
