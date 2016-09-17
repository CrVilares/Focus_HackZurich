using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CarComponents : MonoBehaviour
{
	public VehicleControl vehicleController;
	public ScreenFlash screenFlashing;
    public Transform handleTrigger;
    public Transform door;
    public Transform sitPoint;
    public Transform driver;

    public CameraViewSetting cameraViewSetting;


    private Animator driverAnimator;
    [HideInInspector]
    public bool driving = true;

    [System.Serializable]
    public class CameraViewSetting
    {
        public List<Transform> cameraViews;

        public float distance = 5.0f;
        public float height = 1.0f;
        public float Angle = 20;
    }


    void Start()
    {
        if (!driver) return;

        driverAnimator = driver.GetComponent<Animator>();

        driverAnimator.SetFloat("CarStatus", 4);
        driverAnimator.SetBool("DriveCar", true);
        driverAnimator.Play("Drive Car", 0);


    }



    void Update()
    {

        if (!driver) return;

        if (driving)
        {
            driver.position = sitPoint.position;
            driver.rotation = sitPoint.rotation;
        }
        else
        {

            driver.position = handleTrigger.position;
            driver.rotation = handleTrigger.rotation;


            driverAnimator.enabled = false;


            Component[] Rigidbodys = driver.GetComponentsInChildren(typeof(Rigidbody));

            foreach (Rigidbody rigidbody in Rigidbodys)
            {
                rigidbody.isKinematic = false;
            }

            Component[] Colliders = driver.GetComponentsInChildren(typeof(Collider));

            foreach (Collider collider in Colliders)
            {
                collider.enabled = true;
            }


            Destroy(driver.gameObject, 10.0f);
            driver.parent = null;
            driver = null;


        }


    }




	void OnCollisionEnter(Collision collision) {
		Debug.Log ("crash");
		screenFlashing.state = ScreenFlash.FLASHSTATE.UP;
		//Time.timeScale = 0; // this will freeze the game, stop

		/* activate break */
		vehicleController.myRigidbody.velocity = Vector3.zero;
		vehicleController.myRigidbody.angularVelocity = Vector3.zero;
		vehicleController.brake = true;

		/* Mute motor sounds on collision */
		vehicleController.carSounds.nitro.mute = true;
		vehicleController.carSounds.IdleEngine.mute = true;

		/* take control of own car, disable vehicleController*/
		vehicleController.enabled = false;
	}

}
