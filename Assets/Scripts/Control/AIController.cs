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

        Fighter fighter;
        Mover mover;
        Health health;
        GameObject player;

        Vector3 guardPosition;

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
                fighter.Attack(player);
            }
            else 
            {
                mover.StartMoveAction(guardPosition);
            }
        }

        // Called by Unity Editor
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
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
