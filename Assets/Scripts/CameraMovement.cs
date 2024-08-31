using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] float smoothRatio = 0f;
    [SerializeField] Transform target;

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothRatio);

        transform.position = smoothPos;
    }
}
