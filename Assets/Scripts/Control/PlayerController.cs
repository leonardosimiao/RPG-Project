using UnityEngine;
using RPG.Movement;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        // Update is called once per frame.
        void Update()
        {
            // Will be true while left mouse button is pressed down.
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            Ray rayToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit click;
            bool hasClicked = Physics.Raycast(rayToMouse, out click);

            if (hasClicked)
            {
                GetComponent<Mover>().MoveTo(click.point);
            }
        }
    }
}