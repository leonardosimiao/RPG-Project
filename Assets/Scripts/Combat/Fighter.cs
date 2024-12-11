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
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        // Animation Event called on Unity, required for attack synchronization.
        private void Hit()
        {
            targetHealth.TakeDamage(weaponDamage);
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            targetHealth = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }
    }
}