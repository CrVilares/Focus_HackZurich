using UnityEngine;

public class NPC : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    private int randomSpeed;
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = transform.GetComponent<Animator>();
        targetNav.parent = null;
        this.navMeshAgent = this.GetComponentInChildren<NavMeshAgent>();
        randomSpeed = Random.Range(1, 6);

        if (randomSpeed == 5)
        {
            this.navMeshAgent.speed = 5.0f;
        }
        else
        {
            this.navMeshAgent.speed =2.0f;
        }

        this.navMeshAgent.avoidancePriority = Random.Range(0, 100);
    }


    private GameObject player;
    public float clearDistance = 110.0f;
    public Transform targetNav;
    bool findTarget;


    public float hhspeed;
    protected virtual void FixedUpdate()
    {



        if (player)
        {

            if (Vector3.Distance(transform.position, player.transform.position) > clearDistance)
            {
                Destroy(gameObject);
            }

        }


        if (navMeshAgent.isOnOffMeshLink)
        {

            navMeshAgent.speed = 0;
        }



        if (targetNav != null)
        {


            if (Vector3.Distance(transform.position, targetNav.position) < 10.0f)
            {
                findTarget = true;
                this.navMeshAgent.enabled = true;
                Destroy(targetNav.gameObject);
            }


            if (!findTarget)
            {
                var newRotation = Quaternion.LookRotation(targetNav.position - transform.position);
                newRotation.z = 0.0f;
                newRotation.x = 0.0f;
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5);

                transform.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 800.0f);
            }


           
        }

        hhspeed = this.navMeshAgent.speed;
        if (findTarget)
        {
            animator.SetFloat("speed", this.navMeshAgent.speed);
        }
        else
        {
            animator.SetFloat("speed", GetComponent<Rigidbody>().velocity.magnitude);

        }

        
        if (this.navMeshAgent.enabled)
        {
            if (this.navMeshAgent.pathPending)
                return;
            this.MuckAbout();
        }
        

      //  Debug.DrawLine(transform.position, posr, Color.red);

    }




   // Vector3 posr;
    protected virtual void MuckAbout()
    {
        if (this.navMeshAgent.desiredVelocity.magnitude < 0.2f)
        {
            this.navMeshAgent.SetDestination(GetRandomTargetPoint());
           
            // print("next point");
        }
    }


    private Vector3 minBoundsPoint;
    private Vector3 maxBoundsPoint;
    private float boundsSize = float.NegativeInfinity;
    private Vector3 GetRandomTargetPoint()
    {
        if (boundsSize < 0)
        {
            minBoundsPoint = Vector3.one * float.PositiveInfinity;
            maxBoundsPoint = -minBoundsPoint;
            var vertices = NavMesh.CalculateTriangulation().vertices;
            foreach (var point in vertices)
            {
                if (minBoundsPoint.x > point.x)
                    minBoundsPoint = new Vector3(point.x, minBoundsPoint.y, minBoundsPoint.z);
                if (minBoundsPoint.y > point.y)
                    minBoundsPoint = new Vector3(minBoundsPoint.x, point.y, minBoundsPoint.z);
                if (minBoundsPoint.z > point.z)
                    minBoundsPoint = new Vector3(minBoundsPoint.x, minBoundsPoint.y, point.z);
                if (maxBoundsPoint.x < point.x)
                    maxBoundsPoint = new Vector3(point.x, maxBoundsPoint.y, maxBoundsPoint.z);
                if (maxBoundsPoint.y < point.y)
                    maxBoundsPoint = new Vector3(maxBoundsPoint.x, point.y, maxBoundsPoint.z);
                if (maxBoundsPoint.z < point.z)
                    maxBoundsPoint = new Vector3(maxBoundsPoint.x, maxBoundsPoint.y, point.z);
            }
            boundsSize = Vector3.Distance(minBoundsPoint, maxBoundsPoint);
        }
        var randomPoint = new Vector3(
            Random.Range(minBoundsPoint.x, maxBoundsPoint.x),
            Random.Range(minBoundsPoint.y, maxBoundsPoint.y),
            Random.Range(minBoundsPoint.z, maxBoundsPoint.z)
        );
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, boundsSize, 1);
      //  posr = hit.position;
        return hit.position;
    }




    public Transform playerRoot;

    void DisableRagdoll(bool active)
    {


        Component[] transforms = playerRoot.GetComponentsInChildren(typeof(Rigidbody));

        foreach (Rigidbody t in transforms)
        {
            t.isKinematic = !active;
        }


        Component[] transforms2 = playerRoot.GetComponentsInChildren(typeof(Collider));

        foreach (Collider t in transforms2)
        {
            t.enabled = active;
        }

    }




    void OnCollisionEnter(Collision collision)
    {


        if ((collision.collider.CompareTag("Vehicle") || collision.transform.root.GetComponent<VehicleControl>()) && collision.rigidbody.velocity.magnitude > 10.0f)
        {

            this.navMeshAgent.enabled = false;
            transform.GetComponent<Animator>().enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<Collider>().isTrigger = true;
            DisableRagdoll(true);
            Destroy(gameObject, 10.0f);

        }


    }




}
