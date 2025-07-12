using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("References")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    private bool isGrounded;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        // ???????? ?????? ?? ????????
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void OnEnable()
    {
        // ????????????? ?? ??????? ?????
        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        // ???????????? ?? ??????? ?????
        jumpAction.performed -= OnJump;
    }

    private void Update()
    {
        // ????????? ???? ????????
        moveInput = moveAction.ReadValue<Vector2>();

        // ???????? ?????????? ?? ?????
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ???????? ?????????
        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        // ????????? ???????? ? FixedUpdate ??? ??????
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ???????????? ???? ???????? ????? ? ?????????
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}