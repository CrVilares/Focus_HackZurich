using UnityEngine;
using System.Collections;

public class gameOverscript : MonoBehaviour {

	public TextMesh firstline;
	public TextMesh secondline;

	// Use this for initialization
	void Start () {
		firstline.text = "left: " + ApplicationModel.leftDoorCount + "   mirror: " + ApplicationModel.backMirrorCount + "   right: " + ApplicationModel.rightDoorCount;
		secondline.text = "tacho: " + ApplicationModel.tachoCount + "    phone: " + ApplicationModel.phoneCount;
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
