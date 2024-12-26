using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionDuration = 5f;

        Fighter fighter;
        Mover mover;
        Health health;
        GameObject player;

        Vector3 guardPosition;
        float timeSinceLastSuspicion = Mathf.Infinity;
        
                // Called by Unity Editor
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
        
        //Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!health.IsAlive()) return;
            if (IsPlayerInChasingDistance() && fighter.CanAttack(player))
            {
                timeSinceLastSuspicion = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSuspicion < suspicionDuration)
            {
                SuspicionBehaviour();
                timeSinceLastSuspicion += Time.deltaTime;
            }
            else
            {
                GuardBehaviour();
            }
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool IsPlayerInChasingDistance()
        {
            return DistanceToPlayer() < chaseDistance;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }   
}
