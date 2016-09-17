using UnityEngine;
using System.Collections;

public class raycastHit : MonoBehaviour {
	public Camera camera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			Debug.Log (hit.collider.gameObject.name);
			if (hit.collider.gameObject.name == "LeftDoor_") {
				ApplicationModel.leftDoorCount++;
			} else if (hit.collider.gameObject.name == "RightDoor_") {
				ApplicationModel.rightDoorCount++;
			} else if (hit.collider.gameObject.name == "BackMirror_") {
				ApplicationModel.backMirrorCount++;
			} else if (hit.collider.gameObject.name == "Tacho_") {
				ApplicationModel.tachoCount++;
			} else if (hit.collider.gameObject.name == "SmartPhone_") {
				ApplicationModel.phoneCount++;
			}
		}

		Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
	}

}
