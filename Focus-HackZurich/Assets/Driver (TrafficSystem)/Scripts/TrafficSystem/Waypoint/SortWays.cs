using UnityEngine;
using System.Collections;

public class SortWays : MonoBehaviour
{
    void OnDrawGizmos()
    {
        int count = 1;
        Gizmos.color = new Color(1, 0.5f, 0);
        foreach (Transform node in transform)
        {
            if (node.name != "Way-" + count.ToString())
                node.name = "Way-" + count.ToString();

            count++;
        }

    }

}
