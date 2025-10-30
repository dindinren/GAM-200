using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AmbianceController : MonoBehaviour
{
    static AmbianceController instance;
    public AudioSource ambianceSource;
    public AudioClip ambianceClip;
    [SerializeField, Range(0, 1)] public float volume;
    float tempVolumeHolder;


    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        tempVolumeHolder = volume;
        volume = 0f;

        ambianceSource.clip = ambianceClip;
        ambianceSource.Play();

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
            ambianceSource.volume = volume;
        }
        else
        {
            if (volume <= tempVolumeHolder)
            {
                volume += Time.deltaTime;
                ambianceSource.volume = volume;
            }
        }
    }

}
