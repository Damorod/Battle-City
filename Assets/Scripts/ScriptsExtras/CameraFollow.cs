using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    
    public Vector3 offset;
    public float speed = 0.1f;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 desiredPos = player.position + offset;
        Vector3 smooth = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, speed);
        transform.position = smooth;
    }
}
