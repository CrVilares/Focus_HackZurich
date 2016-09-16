using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;


public class BikeComponents : MonoBehaviour
{

    public Transform handleTrigger;
    public Transform sitPoint;


    public Transform driver;
    private Animator driverAnimator;
    [HideInInspector]
    public bool driving = true;

    public CameraViewSetting cameraViewSetting;


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

        driverAnimator.SetFloat("BikeStatus", 2);
        driverAnimator.SetBool("DriveBike", true);
        driverAnimator.Play("Drive Bike", 0);


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





}
