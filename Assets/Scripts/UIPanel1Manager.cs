using TMPro;
using UnityEngine;

public class UIPanel1Manager : MonoBehaviour
{
    [Header("PACKAGES_UI")]
    public TextMeshProUGUI packageAmtText;
    //[Header("WEIGTH_UI")]
    //public TextMeshProUGUI weightAmtText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PackagesToBeShown();
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

    //Weight Controller

}
