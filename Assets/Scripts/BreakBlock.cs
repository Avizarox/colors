using System.Collections;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    PlatformerController platformerController;
    public float destroyTime = 2f;
    private Animator animator;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 10;
        platformerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerController>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
        {
            if(platformerController.isSliding)
            {
                foreach (var col in GetComponents<Collider2D>())
                {
                    Destroy(col);
                }

                //StartCoroutine(PauseForRealSeconds(0.2f)); так называемые импакт фреймс 8)

                animator.SetTrigger("Break");

                Destroy(gameObject, destroyTime);
            } else
            {
                return;
            }
        }
    }

    IEnumerator PauseForRealSeconds(float seconds)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
    }
}
