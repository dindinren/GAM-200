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
    private bool checkComplete;
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

        checker = 0;
    }
    private void Update()
    {
        if(Dialogue_NPC.dialogueEnded == true && NewReceipientManager.showResult == true)
        {
            PackagesCompleted();
        }
    }
    public void PackagesCompleted()
    {
        checkComplete = false;
        if (checker == requiredNumber)
        {
            ResultScreen();

            SoundManager.PlaySound(SoundType.DAYCOMPLETE);
            //Time.timeScale = 0f;
        }
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
    public void ResultScreen()
    {
        completedScreen.SetActive(true);
        if (Input.GetMouseButton(0) && checkComplete == false)
        {
            PlayerPerformanceResult();
            checkComplete = true;
        }
    }

}
