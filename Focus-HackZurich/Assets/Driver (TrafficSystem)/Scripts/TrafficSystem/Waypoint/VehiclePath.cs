using UnityEngine;
using System.Collections;

public class VehiclePath : MonoBehaviour
{
    public Color pathColor = new Color(1, 0.5f, 0);
    private Transform firstWaypoint;
    void Start()
    {

        int waypointId = 0;

        foreach (Transform waypoint in transform)
        {
            if (waypointId == 0)
            {
                firstWaypoint = waypoint;
                waypointId++;
            }
            waypoint.GetComponent<Node>().mode = firstWaypoint.GetComponent<Node>().mode;
        }

    }


    void OnDrawGizmos()
    {

        int waypointId = 1;
        Gizmos.color = pathColor;

        foreach (Transform waypoint in transform)
        {

            if (waypointId == 1)
            {
                waypoint.GetComponent<Node>().firistNode = true;
                waypoint.GetComponent<Node>().lastNode = false;
            }
            else if (waypointId == transform.childCount)
            {
                waypoint.GetComponent<Node>().firistNode = false;
                waypoint.GetComponent<Node>().lastNode = true;
            }
            else
            {
                waypoint.GetComponent<Node>().firistNode = false;
                waypoint.GetComponent<Node>().lastNode = false;
            }


            if (!waypoint.GetComponent<Node>())
            {
                waypoint.gameObject.AddComponent<Node>();
                waypoint.gameObject.GetComponent<Node>().nodeColor = pathColor;
                waypoint.gameObject.GetComponent<Node>().parentPath = this.name;
            }
            else
            {
                waypoint.gameObject.GetComponent<Node>().nodeColor = pathColor;
                waypoint.gameObject.GetComponent<Node>().parentPath = this.name;
            }


            if (waypoint.name != waypointId.ToString())
                waypoint.name = waypointId.ToString();

            waypointId++;

            Transform nextNode = transform.FindChild(waypointId.ToString());
            Transform beforeNode = transform.FindChild((waypointId - 2).ToString());

            if (beforeNode)
            {
                waypoint.GetComponent<Node>().previousNode = beforeNode;
            }

            if (nextNode)
            {
                Gizmos.DrawLine(waypoint.position, nextNode.position);
                waypoint.GetComponent<Node>().nextNode = nextNode;
            }



        }

    }


}
