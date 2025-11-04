using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneChange_MainMenu : MonoBehaviour
{
    public Animator transition;
    //public AudioSource bgmSource;
    //public float volume;

    [Header("-----------------------")]
    public string SceneName;


    private void Awake()
    {
        //transition = GameObject.Find("Canvas").GetComponentInChildren<Animator>();
    }
    private void Start()
    {

    }
    private void Update()
    {
       // AudioFade();
    }
    public void StartGame()
    {
        StartCoroutine(RightToLeftTransit());
        SceneChange.changed = true;
        //changeScene = true;
    }

    #region Transition

    IEnumerator LeftToRightTransit()
    {
        //play the transition start
        transition.Play("CrossFade_LeftToRight_Start");
        //wait for a while
        yield return new WaitForSeconds(1);
        //load 
        SceneManager.LoadScene(SceneName);
    }

    IEnumerator RightToLeftTransit()
    {
        //play the transition start
        transition.Play("CrossFade_RightToLeft_Start");
        //wait for a while
        yield return new WaitForSeconds(1);
        //load 
        SceneManager.LoadScene(SceneName);
    }

    #endregion Transition


    //void AudioFade()
    //{
    //    if (changeScene)
    //    {
    //        volume -= Time.deltaTime;
    //        bgmSource.volume = volume;
    //    }
    //}

}
