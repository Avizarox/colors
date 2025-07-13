using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isActivated = false;
    public bool isDefault = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
        }
    }

    public static void ResetSpawnPoints()
    {
        SpawnPoint[] allPoints = FindObjectsOfType<SpawnPoint>();

        foreach (var point in allPoints)
        {
            point.isActivated = point.isDefault;
        }
    }

    public static Transform GetDefaultSpawnPoint()
    {
        foreach (var point in FindObjectsOfType<SpawnPoint>())
        {
            if (point.isDefault)
                return point.transform;
        }

        return null;
    }
}



