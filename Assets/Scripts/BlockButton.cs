using UnityEngine;

public class BlockButton : MonoBehaviour
{
    public GameObject[] blocksToDisable;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("ButtonDown");

            foreach (GameObject block in blocksToDisable)
            {
                block.SetActive(false);
            }
        }
    }
}
