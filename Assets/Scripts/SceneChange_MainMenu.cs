using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange_MainMenu : MonoBehaviour
{
    public Animator transition;
    [Header("-----------------------")]
    public string SceneName;


    private void Awake()
    {
        //transition = GameObject.Find("Canvas").GetComponentInChildren<Animator>();
    }
    private void Start()
    {
    }
    public void StartGame()
    {
        StartCoroutine(RightToLeftTransit());
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

    //void Checker()
    //{
    //    switch (SceneManager.GetActiveScene().name)
    //    {

    //        case "WarehouseOutside":
    //            StartCoroutine(RightToLeftTransit());
    //            break;
    //        case "WarehouseV2":
    //            StartCoroutine(LeftToRightTransit());
    //            break;
    //    }
    //}

}
