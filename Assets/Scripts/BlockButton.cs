using UnityEngine;

public class BlockButton : MonoBehaviour
{
    public GameObject[] blocksToDisable;

    private Animator animator;

    public AudioClip buttonSound;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (buttonSound != null && buttonSound.length > 0f)
            {
                audioSource.clip = buttonSound;
                audioSource.volume = 0.3f;
                audioSource.loop = false;
                audioSource.Play();
            }

            animator.SetTrigger("ButtonDown");

            foreach (GameObject block in blocksToDisable)
            {
                block.SetActive(false);
            }
        }
    }
}
