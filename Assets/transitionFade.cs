using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class transitionFade : MonoBehaviour
{
    public Animator transition;
    //public string SceneName;
    public static transitionFade Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // If another instance already exists, destroy this duplicate
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set this as the single instance
            Instance = this;
            // Optional: keep the object alive when loading new scenes
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    public void RightToLeftTransition(string sN)
    {
        StartCoroutine(RightToLeftTransit(sN));
    }
    public void LeftToRightTransition(string sN)
    {
        StartCoroutine(LeftToRightTransit(sN));
    }

    public IEnumerator LeftToRightTransit(string sceneName)
    {
        //play the transition start
        transition.Play("CrossFade_LeftToRight_Start");
        //wait for a while
        yield return new WaitForSeconds(1);
        //load 
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator RightToLeftTransit(string sceneName)
    {
        //play the transition start
        transition.Play("CrossFade_RightToLeft_Start");
        //wait for a while
        yield return new WaitForSeconds(1);
        //load 
        SceneManager.LoadScene(sceneName);
    }
}
