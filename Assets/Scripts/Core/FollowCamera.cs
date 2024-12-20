using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        // LateUpdate is called once per frame, after Update.
        // This guarantees that the camera frame will only
        // update after all frame modifications are ready
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}