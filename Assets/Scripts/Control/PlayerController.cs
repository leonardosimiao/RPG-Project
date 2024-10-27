using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform target;

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