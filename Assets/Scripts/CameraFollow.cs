using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float distance = 6f;
    [SerializeField] private float height = 2f;

    [SerializeField] private float mouseSensitivity = 3f;

    [SerializeField] private float minVerticalAngle = -20f;
    [SerializeField] private float maxVerticalAngle = 60f;

    private float horizontalAngle = 0f;
    private float verticalAngle = 15f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        horizontalAngle += mouseX;
        verticalAngle -= mouseY;

        verticalAngle = Mathf.Clamp(
            verticalAngle,
            minVerticalAngle,
            maxVerticalAngle
        );

        Quaternion rotation = Quaternion.Euler(
            verticalAngle,
            horizontalAngle,
            0f
        );

        Vector3 center = target.position + Vector3.up * height;

        transform.position =
            center - rotation * Vector3.forward * distance;

        transform.LookAt(center);
    }
}