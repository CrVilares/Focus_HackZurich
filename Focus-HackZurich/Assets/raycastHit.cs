using UnityEngine;
using System.Collections;

public class raycastHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, -Vector3.up, out hit))
			Debug.Log("Found an object - distance: " + hit.collider.name);
	}
}
