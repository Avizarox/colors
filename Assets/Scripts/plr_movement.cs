using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float slideForce = 20f;
    [SerializeField] private float groundCheckRadius = 0.05f, sideCheckRadius = 0.1f;

    [Header("References")]
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private int dir = 1;
    //private float horizontalInput;
    private bool isGrounded, isRightBlocked, isLeftBlocked;
    private bool isFacingRight = true;
    private bool jumpRequested, slideRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            slideRequested = true;
        }

        isGrounded = Physics2D.OverlapCircle(transform.position + new Vector3(0f, -0.6f, 0f), groundCheckRadius, groundLayer);
        isLeftBlocked = Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, 0f, 0f), sideCheckRadius, groundLayer);
        isRightBlocked = Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0f, 0f), sideCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        if (isLeftBlocked && dir != 1)
        {
            dir = 1;
            Flip();
        }
        else if (isRightBlocked && dir != -1)
        {
            dir = -1;
            Flip();
        }

        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpRequested = false;
            isGrounded = false;
        }
        if (slideRequested)
        {
            rb.AddForce(new Vector2(dir*slideForce, 0), ForceMode2D.Impulse);
            slideRequested = false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}