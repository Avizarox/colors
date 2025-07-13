using UnityEngine;

public class Danger : MonoBehaviour
{
    public float damageCooldown = 1f;
    private float lastDamageTime = -Mathf.Infinity;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (Time.time - lastDamageTime >= damageCooldown)
            {
                DeathInit deathInit = collision.GetComponent<DeathInit>();
                if (deathInit != null)
                {
                    deathInit.TakeDamage(1);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
