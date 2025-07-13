using System.Collections;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip winSound;

    public GameObject winScreen;
    public GameObject titrs;
    public GameObject logo;

    private SpriteRenderer spriteRendererWinScreen;
    private SpriteRenderer spriteRendererLogo;
    private SpriteRenderer spriteRendererTitrs;

    private bool soundPlayed = false;

    private void Start()
    {
        spriteRendererWinScreen = winScreen.GetComponent<SpriteRenderer>();
        spriteRendererWinScreen.enabled = false;

        spriteRendererTitrs = titrs.GetComponent<SpriteRenderer>();
        spriteRendererTitrs.enabled = false;

        spriteRendererLogo = logo.GetComponent<SpriteRenderer>();
        spriteRendererLogo.enabled = false;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (winSound != null && winSound.length > 0f && soundPlayed == false)
            {
                soundPlayed = true;

                if (MusicManager.Instance != null)
                {
                    MusicManager.Instance.StopMusic();
                }

                audioSource.clip = winSound;
                audioSource.volume = 0.4f;
                audioSource.loop = false;
                audioSource.Play();
            }

            StartCoroutine(QuitGame());
        }
    }

    IEnumerator QuitGame()
    {
        TimerManager.Instance.WinTime();

        spriteRendererTitrs.sortingOrder = 10;
        spriteRendererWinScreen.sortingOrder = 3;
        spriteRendererLogo.sortingOrder = 10;
        spriteRendererTitrs.enabled = true;
        spriteRendererWinScreen.enabled = true;
        spriteRendererLogo.enabled = true;


        yield return new WaitForSeconds(20.0f);

        //Application.Quit();
    }
}
