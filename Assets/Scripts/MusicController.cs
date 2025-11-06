using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.activeSceneChanged += ChangedActiveScene;
        instance = this;
    }
    private void ChangedActiveScene(Scene current, Scene next) //here cus the build does not sometimes play the music 
    {
        SceneChange.changed = false;
        isPlaying = true;
    }


    public void Start()
    {
        StartAudio();

    }
    private void Update()
    {
        AudioFade();
        StopMusic();
        //print(SceneManager.GetActiveScene().name + " SceneChange.changed " + SceneChange.changed + ", volume " + volume + ", tempVolumeHolder " + tempVolumeHolder + ", isPlaying " + isPlaying);
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
