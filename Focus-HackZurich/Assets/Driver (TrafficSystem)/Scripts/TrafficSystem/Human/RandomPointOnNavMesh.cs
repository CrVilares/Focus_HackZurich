// RandomPointOnNavMesh.cs
using UnityEngine;
using System.Collections;
public class RandomPointOnNavMesh : MonoBehaviour
{
    void Awake()
    {
        NavMeshHit closestHit;


        if (NavMesh.SamplePosition(transform.position, out closestHit, 100f, 1))
        {
            gameObject.transform.position = closestHit.position;

        }
        else
        {
            Debug.LogError("Could not find position on NavMesh!");
        }

        
    }
}