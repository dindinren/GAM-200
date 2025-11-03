using UnityEngine;
using System.Collections;
using TMPro;
public class DayManager : MonoBehaviour
{
    //Idealy it would keep track of different days 
    //but for now it just checks for one day ig

    public NewReceipientManager NewReceipientManager;
    public PlayerManagement playerManger;
    [Header("SCREENS")]
    public GameObject completedScreen;
    //public GameObject fail;
    //public GameObject pass;
    public Animator anim;

    [Header("TEXT COMPONENTS")]
    public TextMeshProUGUI money;
    public TextMeshProUGUI packages;

    [Header("---------------")]
    public int requiredMoney;
    public static int checker;



    private int requiredNumber;
    private bool result_has_played;
    bool musicPlaying = false;
    bool key_is_pressed = false;


    private void Awake()
    {
        NewReceipientManager = GameObject.FindGameObjectWithTag("Receipient").GetComponent<NewReceipientManager>();
        playerManger = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();

        requiredNumber = WarehouseSpawnManager.toSpawnPackages.Count;
        Debug.Log($"required num. {requiredNumber}");

    }
    private void Start()
    {
        completedScreen.SetActive(false);
        //fail.SetActive(false);
        //pass.SetActive(false);

        result_has_played = false;

        checker = 0;
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
        yield return new WaitForSeconds(1.3f);

        packages.gameObject.SetActive(true);
        packages.text = checker.ToString();

        yield return new WaitForSeconds(1.5f);
        money.gameObject.SetActive(true);
        money.text = $"${GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>().GetMoneySatus().ToString()}";


        yield return new WaitForSeconds(3);
        PlayerPerformanceResult();

        yield return new WaitForSeconds(0.1f);
        result_has_played = true;
    }
    
    void PlayerPerformanceResult()
    {
        if (playerManger.GetMoneySatus() >= requiredMoney)
        {
            anim.Play("ClipboardCompletedScreen_pass");
            //pass.SetActive(true);
        }
        else 
        {
            anim.Play("ClipboardCompletedScreen_fail");
            //fail.SetActive(true);
        }
    }
    
}
