using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform orientation;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 viewDir = Camera.main.transform.forward;
        viewDir.y = 0f;
        orientation.forward = viewDir.normalized;

        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        Debug.Log("Orientation: " + orientation.forward + " | Camera: " + Camera.main.name);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, 500 * Time.fixedDeltaTime);
        }
    }
}
