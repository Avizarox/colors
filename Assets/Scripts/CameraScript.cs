using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Следование")]
    public Transform target;
    public float smoothSpeed = 0.125f;
    private Vector3 offset = new Vector3(0, 0, -1);

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
