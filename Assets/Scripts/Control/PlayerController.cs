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
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Nothing to do here.");
        }

        private bool InteractWithCombat()
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
                return true;
            }
            return false;
        }
        private bool InteractWithMovement()
        {
            RaycastHit cursor;
            // Defines cursor position and identifies if it is inside scene environment bounds.
            bool isCursorInEnv = Physics.Raycast(GetMouseRay(), out cursor);

            if (isCursorInEnv)
            {
                // Will be true while left mouse button is pressed down.
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(cursor.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}