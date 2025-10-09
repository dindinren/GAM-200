using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class NewReceipientManager : MonoBehaviour
{
    private PackageMove packMov;
    private PackageManager packageManager;
    
    public GameObject winScreen; //temp for now until i got a levelmanager up 
    public static int checker;
    public bool completed;
    private int requiredNumber;


    [Header("RECEIPIENT DATA")]
    public int receipientID;

    private void Awake()
    {
        if (packMov == null)
        {
            packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }

        requiredNumber = WarehouseSpawnManager.requiredNumber;
        Debug.Log($"Required Number: {requiredNumber}");

        checker = 0;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PackagesCompleted();
    }

    private void OnMouseDown()
    {
        CheckIfPackageAndReceipientTheSame();
    }

    public void CheckIfPackageAndReceipientTheSame()
    {
        checker += 1;
        Debug.Log($"Checker: {checker}");
        GameObject result = null;
        foreach(GameObject package in packMov.GetAttachedPackagesList())
        {
            if (receipientID == package.GetComponent<PackageManager>().packageID)
            {
                completed = true;
                result = package;
                Debug.Log($"DESTROYED PACKAGE {package.GetComponent<PackageManager>().packageID} for RECEIPIENT {receipientID}");


                break;
            }
        }
        Destroy(result);
        SoundManager.PlaySound(SoundType.DELIVERYCOMPLETE);


    }

    public void PackagesCompleted()
    {
        if(checker == requiredNumber)
        {
            SoundManager.PlaySound(SoundType.DAYCOMPLETE);

            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
