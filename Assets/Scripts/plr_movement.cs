using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerController : MonoBehaviour
{
    public AudioClip jumpSound;
    private AudioSource audioSource;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float slideForce = 20f; //Изначальная сила слайда
    [SerializeField] private float slideAttenuation = 0.05f; //Чем меньше тем медленее затухает сила слайда
    [SerializeField] private float slideDuration = 0.5f; // сколько длится слайд
    [SerializeField] private float groundCheckRadius = 0.1f, sideCheckRadius = 0.1f;

    [Header("References")]
    [SerializeField] private LayerMask groundLayer, blueLayer, redLayer;

    private DeathInit deathInit;
    private Rigidbody2D rb;
    private int dir = 1;
    private bool isGrounded, isRightBlocked, isLeftBlocked, isOnBlue, isOnRed;
    private bool isFacingRight = true;
    private bool jumpRequested, slideRequested;
    private bool wasGrounded;

    public bool isSliding = false;
    private float slideTimer = 0f;

    private Animator animator;

    private Vector2 boxSize = new Vector2(1f, 0.2f);


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathInit = GetComponent<DeathInit>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        wasGrounded = isGrounded;

        isGrounded = Physics2D.OverlapBox(transform.position + new Vector3(0f, -0.6f, 0f), boxSize, 0f, groundLayer | blueLayer | redLayer);

        isOnBlue = Physics2D.OverlapBox(transform.position + new Vector3(0f, -0.5f, 0f), boxSize, 0f, blueLayer);
        isOnRed = Physics2D.OverlapBox(transform.position + new Vector3(0f, -0.5f, 0f), boxSize, 0f, redLayer);

        isLeftBlocked = Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, 0f, 0f), sideCheckRadius, groundLayer) && !isSliding;
        isRightBlocked = Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0f, 0f), sideCheckRadius, groundLayer) && !isSliding;

        if (!wasGrounded && isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isSliding)
        {
            animator.SetBool("IsJumping", true);
            jumpRequested = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
        {
            slideRequested = true;
        }

        if (isOnBlue && (DozaTaker.lastPickedItem == null || DozaTaker.lastPickedItem.Color == DozaTaker.colors.red))
        {
            deathInit.TakeDamage(1);
        }

        if (isOnRed && (DozaTaker.lastPickedItem == null || DozaTaker.lastPickedItem.Color == DozaTaker.colors.blue))
        {
            deathInit.TakeDamage(1);
        }
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

        if (slideRequested)
        {
            isSliding = true;
            slideTimer = slideDuration;
            rb.linearVelocity = new Vector2(dir * slideForce, rb.linearVelocity.y);
            animator.SetBool("IsSliding", true);
            slideRequested = false;
        }

        if (isSliding)
        {
            slideTimer -= Time.fixedDeltaTime;

            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, slideAttenuation), rb.linearVelocity.y);

            if (slideTimer <= 0f)
            {
                isSliding = false;
                animator.SetBool("IsSliding", false);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        }

        if (jumpRequested)
        {
            if (jumpSound != null && jumpSound.length > 0f)
            {
                audioSource.clip = jumpSound;
                audioSource.volume = 0.3f;
                audioSource.loop = false;
                audioSource.Play();
            }

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpRequested = false;
            isGrounded = false;
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