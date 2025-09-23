using UnityEngine;
using System;
public enum SoundType
{
    JUMP,
    DELIVERYCOMPLETE
}

public enum BGM
{
    MENU
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    [SerializeField] private BGMList[] bgmLists;

    static SoundManager instance; //able to call from ANYWHERE

    public AudioSource audioSource;
    public AudioSource bgmSource;


    private void Awake()
    {
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //for sfx
    }
    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(SoundType sound, float volume = 1)//soundType(enum class), volume  
    {
        //for audio
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0,clips.Length)];
        instance.audioSource.PlayOneShot(randomClip,volume);
    }

    public static void PlayBGM(BGM bgm, float volume = 1)
    {
        AudioClip[] clips = instance.bgmLists[(int)bgm].BGMs;

        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        instance.bgmSource.PlayOneShot(randomClip, volume);

    }


#if UNITY_EDITOR //only applies if youre in the unity editor | to prevent errors?
    private void OnEnable()
    {
        //setting names for soundList from the struc list
        string[] names = Enum.GetNames(typeof(SoundType)); 
        Array.Resize(ref soundList, names.Length);
        for(int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
        //setting names for BGM
        string[] names2 = Enum.GetNames(typeof(BGM));
        Array.Resize(ref bgmLists, names2.Length);
        for (int i= 0; i< bgmLists.Length; i++)
        {
            bgmLists[i].name = names2[i];
        }
    }
}
#endif

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [SerializeField] public string name;
    [SerializeField] private AudioClip[] sounds;
    
}
[Serializable]
public struct BGMList
{
    public AudioClip[] BGMs { get => bgms; }
    [SerializeField] public string name;
    [SerializeField] private AudioClip[] bgms;
}
