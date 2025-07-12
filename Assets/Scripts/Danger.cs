using UnityEngine;

public class Danger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DeathInit deathInit = collision.GetComponent<DeathInit>();
            if (deathInit != null)
            {
                deathInit.TakeDamage(1);
            }
        }
    }
}
