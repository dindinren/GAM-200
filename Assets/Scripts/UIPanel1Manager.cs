using TMPro;
using UnityEngine;

public class UIPanel1Manager : MonoBehaviour
{
    public PlayerManagement playerManagement;
    public PackageMove packMov;

    [Header("PACKAGES_UI")]
    public TextMeshProUGUI packageAmtText;
    public TextMeshProUGUI moneyText;

    [Header("WEIGTH_UI")]
    public TextMeshProUGUI weightAmtText;
    public static int currentWeight;
    public int totalWeight;

    private void Awake()
    {
        playerManagement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
        packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void Update()
    {
        PackagesToBeShown();
        WeightController();

        if (Dialogue_NPC.dialogueEnded)
        {
            ShowMeTheMoney();
        }
    }
    

    //Packages Controller
    public void PackagesToBeShown()
    {
        int count = WarehouseSpawnManager.toSpawnPackages.Count - DayManager.checker;
        
        //After the player select finish
        if (WarehouseSpawnManager.playerReadyToGo == true)
        {
            packageAmtText.text = $"{count}";
        }
        else
        {
            packageAmtText.text = "0";
        }
    }


    public void ShowMeTheMoney()
    {
        moneyText.text = $"${playerManagement.GetMoneySatus()}";
    }

    //Weight Controller
    public void WeightController()
    {
        weightAmtText.text = currentWeight.ToString();
        Debug.Log($"currentWeight: {currentWeight}");

        if (currentWeight >= totalWeight - 13)
        {
            playerManagement.jumpForce = 400;
            playerManagement.speed = 4;

        }
        else if (currentWeight >= totalWeight - 2)
        {
            //player cant rlly jump and move that much 
            playerManagement.jumpForce = 10;
            playerManagement.speed = 1;
        }
        else 
        {
            //player original stats
            playerManagement.jumpForce = 650;
            playerManagement.speed = 7;
            Debug.Log("BCKA TO NORMAL");
        }



    }
}
