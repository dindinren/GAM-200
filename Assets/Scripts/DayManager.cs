using UnityEngine;
using System.Collections;
public class DayManager : MonoBehaviour
{
    //Idealy it would keep track of different days 
    //but for now it just checks for one day ig

    public NewReceipientManager NewReceipientManager;
    public PlayerManagement playerManger;
    [Header("SCREENS")]
    public GameObject completedScreen;
    public GameObject fail;
    public GameObject pass;

    [Header("---------------")]
    public int requiredMoney;
    public static int checker;



    private int requiredNumber;
    private bool result_has_played;
    bool key_is_pressed;


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
        fail.SetActive(false);
        pass.SetActive(false);

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
            if(Dialogue_NPC.dialogueEnded == true && result_has_played == false)
            {
                StartCoroutine(PlayResultScreen());
            }
        }
    }

    IEnumerator PlayResultScreen()
    {
        completedScreen.SetActive(true);
        SoundManager.PlaySound(SoundType.DAYCOMPLETE, 0.5f);
        yield return new WaitForSeconds(1);
        PlayerPerformanceResult();
        yield return new WaitForSeconds(0.1f);
        result_has_played = true;
    }
    
    void PlayerPerformanceResult()
    {
        completedScreen.SetActive(false);

        if (playerManger.GetMoneySatus() >= requiredMoney)
        {
            pass.SetActive(true);
        }
        else 
        {
            fail.SetActive(true);
        }
    }
    
}
