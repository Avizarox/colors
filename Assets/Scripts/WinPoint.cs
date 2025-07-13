using System.Collections;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject titrs;

    private SpriteRenderer spriteRendererWinScreen;
    private SpriteRenderer spriteRendererTitrs;

    private void Start()
    {
        spriteRendererWinScreen = winScreen.GetComponent<SpriteRenderer>();
        spriteRendererWinScreen.enabled = false;

        spriteRendererTitrs = titrs.GetComponent<SpriteRenderer>();
        spriteRendererTitrs.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(QuitGame());
        }
    }

    IEnumerator QuitGame()
    {
        TimerManager.Instance.WinTime();

        spriteRendererTitrs.sortingOrder = 5;
        spriteRendererWinScreen.sortingOrder = 3;
        spriteRendererTitrs.enabled = true;
        spriteRendererWinScreen.enabled = true;


        yield return new WaitForSeconds(20.0f);

        //Application.Quit();
    }
}
