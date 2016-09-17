using UnityEngine;
using System.Collections;

public class PhoneRing : MonoBehaviour {

	public AudioSource ring;
	private Timer timer = new Timer(12.0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(timer.UpdateAndTest()){
			ring.Play ();
			timer = new Timer (12.0f);
		}
	}
}
