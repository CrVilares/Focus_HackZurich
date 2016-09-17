using UnityEngine;
using System.Collections;

public class gameOverscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Joystick1Button0)){
			Application.LoadLevel ("SmallCity");
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button1)){
			Application.Quit ();
		}

	}


}
