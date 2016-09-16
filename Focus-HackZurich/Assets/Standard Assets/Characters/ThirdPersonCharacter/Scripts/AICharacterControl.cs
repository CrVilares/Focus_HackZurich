
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for





        // Vector3 posr;
        protected virtual void MuckAbout()
        {
            if (this.agent.desiredVelocity.magnitude < 0.1f)
            {
                this.agent.SetDestination(GetRandomTargetPoint());

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
            NavMesh.SamplePosition(randomPoint, out hit, boundsSize/100.0f, 1);
            //  posr = hit.position;
            return hit.position;
        }





        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            target.parent = null;

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }



        bool findTarget;










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


            if (collision.collider.CompareTag("Vehicle") && collision.rigidbody.velocity.magnitude > 10.0f)
            {


                Destroy(this.GetComponent<AICharacterControl>());



                this.GetComponent<Rigidbody>().isKinematic = true;
                transform.GetComponent<Collider>().isTrigger = true;
                transform.GetComponent<Animator>().enabled = false;

                Destroy(this.agent);

                this.GetComponent<ThirdPersonCharacter>().enabled = false;
                
                
                DisableRagdoll(true);
                

                Destroy(gameObject, 10.0f);

                
            }


        }




        private void Update()
        {


            if (!this.agent.enabled) return;


            if (this.agent.enabled)
            {
                if (this.agent.pathPending)
                    return;
                this.MuckAbout();
            }


            if (target != null)
            {
                agent.SetDestination(target.position);




                if (Vector3.Distance(transform.position, target.position) < 10.0f)
                {
                    findTarget = true;
                    this.agent.enabled = true;
                    Destroy(target.gameObject);
                }


                if (!findTarget)
                {
                    var newRotation = Quaternion.LookRotation(target.position - transform.position);
                    newRotation.z = 0.0f;
                    newRotation.x = 0.0f;
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5);

                    transform.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 800.0f);
                }


            }



            if (agent.remainingDistance > agent.stoppingDistance)
            {
            
                character.Move(agent.desiredVelocity/2.0f, false, false);

            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
