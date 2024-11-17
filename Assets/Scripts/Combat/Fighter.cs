using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat 
{   
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2.5f;
        
        Transform target;

        // Update is called once per frame.
        private void Update()
        {
            if (target == null) return;

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
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event called on Unity, required for attack synchronization.
        private void Hit()
        {

        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}