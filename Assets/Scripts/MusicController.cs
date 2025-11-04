using System;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    public static bool isPlaying;

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
        StartAudio();


        isPlaying = true;

    }
    private void Update()
    {
        AudioFade();
        StopMusic();
    }

    void StartAudio()
    {
        tempVolumeHolder = volume;
        volume = 0.0000001f;

        bgmSource.clip = bgmClip;
        bgmSource.Play();

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
            if (MathF.Abs(volume) <= tempVolumeHolder)
            {
                volume += Time.deltaTime;
                bgmSource.volume = volume;
            }
        }
    }

    public void StopMusic()
    {
        if (!isPlaying)
        {
            bgmSource.Stop();
        }
    }

}
