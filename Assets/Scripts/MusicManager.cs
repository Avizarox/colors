using UnityEngine;
using UnityEngine.LowLevel;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip introClip;
    public AudioClip loopClip;

    public AudioClip secretClip;

    private AudioSource audioSource;

    private bool isPlayingSecret = false;
    private bool secretThemePlayed = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.6f;
    }

    void Start()
    {
        PlayIntroThenLoop();
    }

    private void PlayIntroThenLoop()
    {
        audioSource.clip = introClip;
        audioSource.time = 2f;
        audioSource.loop = false;
        audioSource.Play();
        Invoke(nameof(StartLoop), introClip.length - audioSource.time);
    }

    private void StartLoop()
    {
        if (isPlayingSecret) return;
        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySecretTheme()
    {
        if (isPlayingSecret || secretThemePlayed) return;

        secretThemePlayed = true;
        CancelInvoke();
        isPlayingSecret = true;

        audioSource.volume = 0.8f;

        audioSource.clip = secretClip;
        audioSource.time = 3.2f;
        audioSource.loop = false;
        audioSource.Play();

        Invoke(nameof(ResumeLoopAfterSecret), secretClip.length);
    }

    private void ResumeLoopAfterSecret()
    {
        audioSource.volume = 0.6f;
        isPlayingSecret = false;
        StartLoop();
    }

    public void StopMusic()
    {
        CancelInvoke();
        audioSource.Stop();
        isPlayingSecret = false;
    }
}
