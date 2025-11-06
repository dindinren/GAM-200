using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public GameObject player;
    public TimeManager timeManager;
    [Header("-----------------------")]
    public string SceneName;
    public static bool changed;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transition = GameObject.Find("CrossFade").GetComponent<Animator>();
        timeManager = GameObject.Find("Parcel_UI_1").GetComponent<TimeManager>();
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    private void Start()
    {
        //changed = false;
    }

    private void ChangedActiveScene(Scene current, Scene next) //here cus the build does not sometimes play the music 
    {
        changed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TO ADD: player reaches the trigger area, make it so they cant move 

        if(collision.CompareTag("Player"))
        {
            Checker();
            DontDestroyOnLoad(player);
            //DontDestroyOnLoad(timeManager);
            
            //time advancement
            StartCoroutine(TimeAdvance());

            changed = true;

        }
    }

    IEnumerator TimeAdvance()
    {
        yield return new WaitForSeconds(1f);
        timeManager.TimeAdvance();
        Debug.Log("TIme has passed");
    }

    #region Transition

    IEnumerator LeftToRightTransit()
    {
        //play the transition start
        transition.Play("CrossFade_LeftToRight_Start",0);
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
