using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
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
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray rayToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit click;
        bool hasClicked = Physics.Raycast(rayToMouse, out click);

        if (hasClicked)
        {
            GetComponent<NavMeshAgent>().destination = click.point;
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
