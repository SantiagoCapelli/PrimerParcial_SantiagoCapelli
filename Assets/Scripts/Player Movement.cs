using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Camara")]
    [SerializeField] private Transform cameraTransform;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody rb;

    private float horizontal;
    private float vertical;

    private bool grounded;
    private bool isDashing;
    private bool canDash = true;

    private Vector3 dashDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Vector3 velocity = rb.linearVelocity;

            velocity.x = dashDirection.x * dashSpeed;
            velocity.z = dashDirection.z * dashSpeed;

            rb.linearVelocity = velocity;
            return;
        }

        Move();

        // Solo rota cuando el jugador se está moviendo
        if (horizontal != 0 || vertical != 0)
        {
            RotatePlayer();
        }
    }

    private void Move()
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 direction =
            cameraForward * vertical +
            cameraRight * horizontal;

        direction.Normalize();

        Vector3 velocity = rb.linearVelocity;

        velocity.x = direction.x * moveSpeed;
        velocity.z = direction.z * moveSpeed;

        rb.linearVelocity = velocity;
    }

    private void RotatePlayer()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;

        if (forward.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(forward);
            rb.MoveRotation(rotation);
        }
    }

    private void Jump()
    {
        Vector3 velocity = rb.linearVelocity;

        velocity.y = jumpForce;
        rb.linearVelocity = velocity;

        grounded = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        dashDirection =
            cameraForward * vertical +
            cameraRight * horizontal;

        // Si no estás apretando WASD, dashea hacia adelante
        if (dashDirection.sqrMagnitude < 0.01f)
        {
            dashDirection = cameraForward;
        }

        dashDirection.Normalize();

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                grounded = true;
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}