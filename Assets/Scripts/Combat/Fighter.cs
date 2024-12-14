using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat 
{   
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 1.5f;
        [SerializeField] float attackCoolDown = 1f;
        [SerializeField] float weaponDamage = 5f;
        
        Transform target;
        Health targetHealth;
        float timeSinceLastAttack = 0;

        // Update is called once per frame.
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (!targetHealth.IsAlive()) return;

            if (!IsTargetInWeaponRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        
        private bool IsTargetInWeaponRange()
        {
            return (Vector3.Distance(transform.position, target.position) < weaponRange);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target);
            if (timeSinceLastAttack > attackCoolDown)
            {
                // This will trigger the Hit() event;
                TriggerAttack("start");
                timeSinceLastAttack = 0;
            }
        }

        //Start or stop attack-related triggers
        private void TriggerAttack(string actionState)
        {
            if (actionState == "start")
            {
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
            }
            else if (actionState == "stop")
            {                
                GetComponent<Animator>().ResetTrigger("attack");
                GetComponent<Animator>().SetTrigger("stopAttack");
            }
        }

        // Animation Event called on Unity, required for attack synchronization.
        private void Hit()
        {
            if (targetHealth == null) return;
            targetHealth.TakeDamage(weaponDamage);
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            Health healthState = combatTarget.GetComponent<Health>();
            return healthState.IsAlive();
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            targetHealth = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            TriggerAttack("stop");
            target = null;
        }
    }
}