using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    public GameObject player;

    public Animator transition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transition = GameObject.Find("Canvas").GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TO ADD: player reaches the trigger area, make it so they cant move 

        if(collision.CompareTag("Player"))
        {
            Checker();
            DontDestroyOnLoad(player);
        }
    }

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

    void Checker()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "WarehouseOutside":
                StartCoroutine(RightToLeftTransit());
                break;
            case "WarehouseV2":
                StartCoroutine(LeftToRightTransit());
                break;
        }
    }

}
