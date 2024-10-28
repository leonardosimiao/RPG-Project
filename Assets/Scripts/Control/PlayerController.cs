using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        // Update is called once per frame.
        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] objectsHitList = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit objectHit in objectsHitList)
            {
                CombatTarget target = objectHit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }
        private void InteractWithMovement()
        {
            // Will be true while left mouse button is pressed down.
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit click;
            bool hasClicked = Physics.Raycast(GetMouseRay(), out click);

            if (hasClicked)
            {
                GetComponent<Mover>().MoveTo(click.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}