using TMPro;
using UnityEngine;

public class UIPanel1Manager : MonoBehaviour
{
    [Header("PACKAGES_UI")]
    public TextMeshProUGUI packageAmtText;
    public TextMeshProUGUI moneyText;


    //[Header("WEIGTH_UI")]
    //public TextMeshProUGUI weightAmtText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //deliveredPackages = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PackagesToBeShown();

        if (Dialogue_NPC.dialogueEnded)
        {
            ShowMeTheMoney();
        }
    }

    //Packages Controller
    public void PackagesToBeShown()
    {
        int count = WarehouseSpawnManager.toSpawnPackages.Count - DayManager.checker;
        //deliveredPackages += 1;
        
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
        PlayerManagement playerManage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
        moneyText.text = $"${playerManage.GetMoneySatus()}";
    }

    //Weight Controller

}
