using UnityEngine;
using System.Collections;

public class tachometer : MonoBehaviour {

	public AIVehicle aiVehicle;
	public TextMesh textmesh;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int speed = Mathf.FloorToInt (aiVehicle.vehicleSpeed);
		textmesh.text = speed.ToString();
		if(speed > 50){
			textmesh.color = Color.red;
		} else {
			textmesh.color = Color.white;
		}

	}
}
