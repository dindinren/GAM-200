using UnityEngine;

public class MusicController : MonoBehaviour
{
    static MusicController instance;
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    [SerializeField, Range(0, 1)] public float volume;
    float tempVolumeHolder;


    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        tempVolumeHolder = volume;
        volume = 0;

        bgmSource.clip = bgmClip;
        bgmSource.Play();

    }
    private void Update()
    {
        AudioFade();
    }

    void AudioFade()
    {
        if (SceneChange.changed)
        {
            volume -= Time.deltaTime;
            bgmSource.volume = volume;
        }
        else
        {
            if (volume <= tempVolumeHolder)
            {
                volume += Time.deltaTime;
                bgmSource.volume = volume;
            }
        }
    }

}
