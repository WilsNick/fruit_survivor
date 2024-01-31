using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object that the camera should follow
    public float smoothSpeed = 0.125f; // The smoothness of camera movement
    public Vector3 offset; // The offset between the camera and the target

    private void FixedUpdate()
    {
        // Calculate the desired position for the camera to move to
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position using Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
