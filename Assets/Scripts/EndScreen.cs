using System.Collections;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        spriteRenderer.sortingOrder = 11;
    }

    public void EndRun()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        spriteRenderer.enabled = true;

        yield return new WaitForSeconds(10.0f);

        spriteRenderer.enabled = false;
    }
}
