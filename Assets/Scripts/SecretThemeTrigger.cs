using UnityEngine;

public class SecretThemeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MusicManager.Instance.PlaySecretTheme();
        }
    }
}
