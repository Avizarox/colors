using System.Collections;
using UnityEngine;

public class DeathInit : MonoBehaviour
{
    public AudioClip deathSound;
    private AudioSource audioSource;

    public int health = 1;
    public Sprite sprite;
    public float respawnTime = 1.5f;
    public float critTime = 5.0f;

    private GameObject player;
    private SpriteRenderer spriteRendered;
    private Animator animator;
    private Rigidbody2D rb;
    private PlatformerController platformerController;

    private SpawnPoint[] spawnPoints;

    private void Start()
    {
        // Получаем игрока и компоненты
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRendered = player.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        platformerController = GetComponent<PlatformerController>();
        audioSource = GetComponent<AudioSource>();

        // Сбрасываем все спаунпоинты в начальное состояние
        SpawnPoint.ResetSpawnPoints();

        // Получаем список всех спаунпоинтов
        spawnPoints = FindObjectsOfType<SpawnPoint>();

        // Перемещаем игрока на дефолтный спаун при запуске
        Transform defaultSpawn = SpawnPoint.GetDefaultSpawnPoint();
        if (defaultSpawn != null)
        {
            player.transform.position = defaultSpawn.position;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (deathSound != null && deathSound.length > 0f)
            {
                audioSource.clip = deathSound;
                audioSource.volume = 0.2f;
                audioSource.loop = false;
                audioSource.Play();
            }

            animator.enabled = false;
            platformerController.enabled = false;
            rb.simulated = false;
            spriteRendered.sprite = sprite;

            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        TimerManager.Instance.ReduceTime(critTime);

        yield return new WaitForSeconds(respawnTime);

        health = 1;

        Transform spawn = GetNearestActivatedSpawnPoint(player.transform.position);
        if (spawn != null)
        {
            player.transform.position = spawn.position;
        }

        rb.simulated = true;
        rb.linearVelocity = Vector2.zero;
        animator.enabled = true;
        platformerController.enabled = true;
    }

    private Transform GetNearestActivatedSpawnPoint(Vector3 fromPosition)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
            return null;

        SpawnPoint nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var sp in spawnPoints)
        {
            if (!sp.isActivated) continue;

            float distance = Vector3.Distance(fromPosition, sp.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = sp;
            }
        }

        return nearest?.transform;
    }
}
