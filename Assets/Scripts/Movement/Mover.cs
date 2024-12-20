using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement {
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;

        // Start is called before the first frame update
        private void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();    
        }
        // Update is called once per frame.
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}