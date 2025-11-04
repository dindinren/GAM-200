using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseSpawnManager : MonoBehaviour
{
    [Header("OBJECTS")]
    public GameObject clipboard;
    public Animator clipboard_darkening;
    public GameObject player;
    public GameObject playerInteractionButton;
    public Button proceedButton;
    public Toggle selection;

    [Header("POSITION")]
    public Transform packageSpawnPoint;

    [Header("LIST")]
    public static List<int> toSpawnPackages = new List<int>();
    private HashSet<int> selectedPackageIDs = new HashSet<int>();

    [Header("PACKAGES DATA")]
    //public List<PackageData> packages; ///with Scriptable Objects
    public List<PackageManager> packages; //with prefab gameobjects

    [Header("DISPLAY DATA")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI reward;
    public TextMeshProUGUI description;
    public TextMeshProUGUI location;

    [Header("-----------------------------------")]
    public static int requiredNumber = 2; //in the future if theres a level mamager change this accordingly
    public static bool playerReadyToGo;


    int index = 0; 

    void Awake()
    {
        // Hide button as early as possible
        if (proceedButton != null)
        {
            proceedButton.gameObject.SetActive(false);
            //Debug.Log("Awake: Button forcefully disabled");
        }

    }

    void Start()
    {
        // Ensure all packages start unselected
        selectedPackageIDs.Clear();
        toSpawnPackages.Clear();
        selection.isOn = false;

        playerReadyToGo = false;


        index = 0;
        if (packages != null && packages.Count > 0)
            SetPackagesData();

        // Force hide the proceed button at start
        if (proceedButton != null)
        {
            proceedButton.gameObject.SetActive(false);
            //Debug.Log("Start: Button forcefully disabled");
        }
    }

    void OnEnable()
    {
        // Also hide when the script becomes enabled
        if (proceedButton != null)
        {
            proceedButton.gameObject.SetActive(false);
            //Debug.Log("OnEnable: Button forcefully disabled");
        }
    }

    void Update()
    {
        ConditionToUnlockProceed();
        //Debug.Log($"toSpawn Count: {toSpawnPackages.Count}, Required: {requiredNumber}, Button Active: {proceedButton.gameObject.activeSelf}");
    }

    void SetPackagesData()
    {
        //name.text = packages[index].packageReceipientName;
        //reward.text = $"Reward: ${packages[index].packageValue}";
        //// Update toggle based on package's own selection state
        //selection.isOn = selectedPackageIDs.Contains(packages[index].packageID);

        ///Doing without Scriptable Objects
        name.text = $"<b>Name:</b>{packages[index].receipientName}";
        reward.text = $"${packages[index].packageValue.ToString()}";
        selection.isOn = selectedPackageIDs.Contains(packages[index].packageID);
        description.text = packages[index].description;
        location.text = packages[index].location;

    }

    public void NextButton()
    {
        index++;
        if (index >= packages.Count)
        {
            index = 0;
        }
        SetPackagesData();

        SoundManager.PlaySound(SoundType.PAGEFLIP);
    }

    public void BackButton()
    {
        index--;
        if (index < 0)
        {
            index = packages.Count - 1;
        }
        SetPackagesData();

        SoundManager.PlaySound(SoundType.PAGEFLIP);
    }

    public void FinishButton()
    {
        clipboard.SetActive(false);
        clipboard_darkening.Play("Clipboard_Darken_End");
        // Spawn only the selected packages
        foreach (int packageID in toSpawnPackages)
        {
            //PackageData packageData = packages.Find(p => p.packageID == packageID);
            PackageManager packageData = packages.Find(p => p.packageID == packageID);
            if (packageData != null && packageData.packagePrefab != null)
            {
                SpawnPackageWithData(packageData);
            }
        }
        SoundManager.PlaySound(SoundType.PARCEL_GIVENOUT);


        playerReadyToGo = true;
        //Debug.Log($"ready bool: {playerReadyToGo}");

        player.GetComponent<PlayerManagement>().enabled = true;

        playerInteractionButton.SetActive(false);

    }

    private void SpawnPackageWithData(PackageManager packageData)
    {
        // Instantiate the package prefab
        //GameObject packageInstance = Instantiate(packageData.type, packageSpawnPoint.position, packageSpawnPoint.rotation, packageSpawnPoint);
        GameObject packageInstance = Instantiate(packageData.packagePrefab, packageSpawnPoint.position, packageSpawnPoint.rotation);


        // Get or add the Package component and set its data
        PackageManager packageComponent = packageInstance.GetComponent<PackageManager>();

        if (packageComponent == null)
        {
            packageComponent = packageInstance.AddComponent<PackageManager>();
        }

        // Set the package data from ScriptableObject
        //packageComponent.recipientName = packageData.packageReceipientName;
        //packageComponent.packageValue = packageData.packageValue;
        //packageComponent.packageID = packageData.packageID;
        //packageComponent.packageHP = packageData.packageHP;

        ///Set the package data from the Prefab List
        packageComponent.receipientName = packageData.receipientName;
        packageComponent.packageID  = packageData.packageID;
        packageComponent.packageHP = packageData.packageHP;
        packageComponent.packageValue = packageData.packageValue;
  
        //Debug.Log($"Spawned packageID of  {packageComponent.packageID} with value ${packageComponent.packageValue} and HP of {packageComponent.packageHP} and type {packageComponent.packagePrefab}");
    }


    public void ConditionToUnlockProceed()
    {
        if(toSpawnPackages.Count == requiredNumber)
        {
            proceedButton.gameObject.SetActive(true);
        }
        else
        {
            proceedButton.gameObject.SetActive(false);
        }
    }

    public void CheckWhichPackageToSpawn()
    {
        int currentSelection = packages[index].packageID;

        if (selection.isOn)
        {
            if (!selectedPackageIDs.Contains(currentSelection))
            {
                selectedPackageIDs.Add(currentSelection);
                toSpawnPackages.Add(currentSelection);
                //Debug.Log($"Package {currentSelection} ADDED. Total: {toSpawnPackages.Count}");
            }
        }
        else
        {
            if (selectedPackageIDs.Contains(currentSelection))
            {
                selectedPackageIDs.Remove(currentSelection);
                toSpawnPackages.Remove(currentSelection);
                //Debug.Log($"Package {currentSelection} REMOVED. Total: {toSpawnPackages.Count}");
            }
        }

        //Debug.Log("AWAITING TO SPAWN PACKAGES:");
        foreach (int packageID in toSpawnPackages)
        {
            //Debug.Log($"Package ID: {packageID}");
        }
    }

    
}