using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public float levelTime = 120f;
    private float timeRemaining;
    public TextMeshProUGUI timer;
    public EndScreen endScreen;

    private bool levelEnded = false;

    public static TimerManager Instance;

    private Color originalColor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        timeRemaining = levelTime;
        originalColor = timer.color;
    }

    private void Update()
    {
        if (levelEnded) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            ReloadLevel();
        }

        GetTimeRemaining();
    }

    public void ReduceTime(float amount)
    {
        timer.color = Color.red;
        StartCoroutine(MyCoroutine());
        timeRemaining -= amount;
        timeRemaining = Mathf.Max(timeRemaining, 0f);
    }

    private void ReloadLevel()
    {
        endScreen.EndRun();
        timer.color = originalColor;
        levelEnded = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GetTimeRemaining() 
    {
        if(timeRemaining < 20)
        {
            timer.color = Color.red;
        }
        timer.text = timeRemaining.ToString("F0");
    }

    public void WinTime()
    {
        timeRemaining = 20f;
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(1.0f);

        timer.color = originalColor;
    }
}
