using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float chargeSpeed = 1f;
    public float maxCharge = 1.5f;
    public float jumpForce = 15f;
    public float maxJumpPower = 20f;
    public bool allowJump = true;
    public Transform orientation;

    private float charge = 0f;
    private bool isCharging = false;
    private bool isGrounded = false;
    private Rigidbody rb;
    private Animator animator;

    private Vector3 inputDirection;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 viewDir = Camera.main.transform.forward;
        viewDir.y = 0;
        orientation.forward = viewDir.normalized;

        inputDirection = (orientation.forward * vertical + orientation.right * horizontal).normalized;


        if (isGrounded && !isCharging)
        {
            moveDirection = inputDirection;
        }
        else if (!isGrounded)
        {
            moveDirection = Vector3.zero;
        }


        bool isWalking = moveDirection != Vector3.zero && isGrounded && !isCharging;
        animator.SetBool("isWalking", isWalking);

        if (allowJump && Input.GetKey(KeyCode.Space) && isGrounded)
        {
            if (!isCharging)
            {
                isCharging = true;
                animator.ResetTrigger("isLanding");
                animator.SetTrigger("isJumping"); 


                Vector3 stopHorizontal = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.linearVelocity -= stopHorizontal;
            }

            charge += Time.deltaTime * chargeSpeed;
            charge = Mathf.Clamp(charge, 0f, maxCharge);
        }


        if (allowJump && Input.GetKeyUp(KeyCode.Space) && isCharging && isGrounded)
        {
            Vector3 jumpDirection = (Vector3.up + inputDirection).normalized;
            float jumpPower = Mathf.Min(charge * jumpForce, maxJumpPower);

            rb.linearVelocity = Vector3.zero;
            rb.AddForce(jumpDirection * jumpPower, ForceMode.Impulse);

            animator.SetTrigger("StartJumpLoop"); 

            isCharging = false;
            charge = 0f;
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && !isCharging)
        {
            Vector3 move = moveDirection * moveSpeed;
            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 500 * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;

                animator.SetTrigger("isLanding");
                return;
            }
        }
    }
}
