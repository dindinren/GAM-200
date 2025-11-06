using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DayManager : MonoBehaviour
{
    //Idealy it would keep track of different days 
    //but for now it just checks for one day ig

    public NewReceipientManager NewReceipientManager;
    public PlayerManagement playerManger;
    //public MusicController music;

    [Header("SCREENS")]
    public GameObject completedScreen;
    public Animator anim;

    [Header("TEXT COMPONENTS")]
    public TextMeshProUGUI money;
    public TextMeshProUGUI packages;

    [Header("BUTTONS")]
    public GameObject interaction_button;

    [Header("---------------")]
    public int requiredMoney;
    public static int checker;

    [Header("TRANSITION")]
    public Animator transition;
    public string SceneName = "TempNewDay"; //RMB TO DELETE ONCE ALL IS FINALIZED


    private int requiredNumber;
    private bool result_has_played;
    private bool musicPlaying = false;
    private bool key_is_pressed = false;
    private bool canPressF = false;


    private void Awake()
    {
        NewReceipientManager = GameObject.FindGameObjectWithTag("Receipient").GetComponent<NewReceipientManager>();
        playerManger = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
        //music = GameObject.Find("SoundManager").GetComponent<MusicController>();
        requiredNumber = WarehouseSpawnManager.toSpawnPackages.Count;
        Debug.Log($"required num. {requiredNumber}");

    }
    private void Start()
    {
        completedScreen.SetActive(false);
        interaction_button.SetActive(false);
        
        result_has_played = false;

        checker = 0;
    }
    private void Update()
    {
        if (canPressF)
        {
            GoToNextDay();
        }
    }

    private void FixedUpdate()
    {
        DayCompleted();

        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
        {
            key_is_pressed = true;
        }
        else
        {
            key_is_pressed = false;
        }

    }

    /// <summary>
    /// 1) When Checker == requiredNumber && dialogue has ended
    /// 2) show day completed scene
    /// 3) when click/press F, it will show how well you've done based on how much money you got
    /// </summary>

    void DayCompleted()
    {
        if(checker == requiredNumber || TimeManager.gameOngoing == false)
        {
            if(Dialogue_NPC.dialogueEnded == true && result_has_played == false && !musicPlaying)
            {
                StartCoroutine(PlayResultScreen());
                SoundManager.PlaySound(SoundType.DAYCOMPLETE);
                MusicController.isPlaying = false;
                musicPlaying = true;
            }
        }
    }

    IEnumerator PlayResultScreen()
    {
        completedScreen.SetActive(true);
        SoundManager.PlaySound(SoundType.DAYCOMPLETE);
        yield return new WaitForSeconds(1.1f);

        packages.gameObject.SetActive(true);
        packages.text = checker.ToString();
        SoundManager.PlaySound(SoundType.SCRIBBLE);

        yield return new WaitForSeconds(1.5f);
        money.gameObject.SetActive(true);
        money.text = $"${GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>().GetMoneySatus().ToString()}";
        SoundManager.PlaySound(SoundType.SCRIBBLE);

        yield return new WaitForSeconds(3);
        PlayerPerformanceResult();
        SoundManager.PlaySound(SoundType.QUOTA);

        yield return new WaitForSeconds(1);
        interaction_button.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        result_has_played = true;
        canPressF = true;
        SceneChange.changed = true;

    }
    
    void PlayerPerformanceResult()
    {
        if (playerManger.GetMoneySatus() >= requiredMoney)
        {
            anim.Play("ClipboardCompletedScreen_pass");
        }
        else 
        {
            anim.Play("ClipboardCompletedScreen_fail");
        }
    }

    void GoToNextDay()
    {
        if(canPressF && key_is_pressed)
        {
            StartCoroutine(RightToLeftTransit());
        }
    }


    IEnumerator LeftToRightTransit()
    {
        //play the transition start
        transition.Play("CrossFade_LeftToRight_Start", 0);
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }

}
