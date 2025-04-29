using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float chargeSpeed = 1f;
    public float maxCharge = 5f;
    public float jumpForce = 10f;

    private float charge = 0f;
    private bool isCharging = false;
    private Rigidbody rb;

    public Transform orientation;

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
        viewDir.y = 0;
        orientation.forward = viewDir.normalized;

        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        if (Input.GetKey(KeyCode.Space))
        {
            isCharging = true;
            charge += Time.deltaTime * chargeSpeed;
            charge = Mathf.Clamp(charge, 0f, maxCharge);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isCharging)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                Vector3 jumpDirection = Vector3.up + moveDirection.normalized;
                rb.AddForce(jumpDirection * charge * jumpForce, ForceMode.Impulse);

                charge = 0f;
                isCharging = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 move = moveDirection.normalized * moveSpeed;
        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontalMove = moveDirection.normalized * moveSpeed;

        rb.linearVelocity = new Vector3(horizontalMove.x, velocity.y, horizontalMove.z);

        if (moveDirection != Vector3.zero)
          {
              Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
              transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 500 * Time.deltaTime);
          }
    }
}
