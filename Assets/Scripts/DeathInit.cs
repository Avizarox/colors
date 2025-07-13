using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class DeathInit : MonoBehaviour
{
    public int health = 1;
    public Sprite sprite;
    public Transform spawnPoint;
    public float respawnTime = 1.5f;
    public float critTime = 5.0f;

    private GameObject player;
    private SpriteRenderer spriteRendered;
    private Animator animator;
    private Rigidbody2D rb;
    private PlatformerController platformerController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRendered = player.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        platformerController = GetComponent<PlatformerController>();

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            animator.enabled = false;
            platformerController.enabled = false;
            rb.simulated = false;
            spriteRendered.sprite = sprite;

            StartCoroutine(Respawn());
        } else {
            return;
        }
    }

    IEnumerator Respawn()
    {
        TimerManager.Instance.ReduceTime(critTime);

        yield return new WaitForSeconds(1.0f);

        health = 1;
        player.transform.position = spawnPoint.position;
        rb.simulated = true;
        rb.linearVelocity = Vector2.zero;
        animator.enabled = true;
        platformerController.enabled = true;
    }

}
