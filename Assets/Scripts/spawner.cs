using UnityEngine;

public class spawner : MonoBehaviour
{
    public AudioClip doseSound;
    private AudioSource audioSource;

    [SerializeField] GameObject Item;
    private bool inPosition;
    private float timer;
    public float timerDuration = 5f;
    void Start()
    {
        inPosition = true;
        timer = timerDuration;
        Instantiate(Item, this.transform);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (inPosition == false)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Instantiate(Item, this.transform);
                    inPosition = true;
                    timer = timerDuration;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.childCount > 0)
            {
                if (doseSound != null && doseSound.length > 0f)
                {
                    audioSource.clip = doseSound;
                    audioSource.volume = 0.3f;
                    audioSource.loop = false;
                    audioSource.Play();
                }

                inPosition = false;
            }
        }
    }
}
