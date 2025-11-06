using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class TempM2Restart : MonoBehaviour
{
    public Animator anim;
    public string SceneName = "MainMenu";


    /// <summary>
    /// Mouse click 
    /// </summary>
    public void OnRestart()
    {
        StartCoroutine(RightToLeftTransit());
    }


    IEnumerator RightToLeftTransit()
    {
        //play the transition start
        anim.Play("CrossFade_RightToLeft_Start");
        //wait for a while
        yield return new WaitForSeconds(1);
        //load 
        SceneManager.LoadScene(SceneName);
    }
}
