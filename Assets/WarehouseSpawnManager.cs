using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.ReloadAttribute;

public class WarehouseSpawnManager : MonoBehaviour
{
    public GameObject clipboard;
    public GameObject player;

    public Button proceedButton;

    public Toggle selection;
    private List<int> toSpawnPackages = new List<int>();
    private HashSet<int> selectedPackageIDs = new HashSet<int>();


    public Transform packageSpawnPoint;
    int index = 0;
    public bool packagesPicked;
    //int requiredNumber = 2;

    [Header("DISPLAY DATA")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI reward;

    [Header("PACKAGES DATA")]
    public List<PackageData> packages;

    void Awake()
    {
        // Hide button as early as possible
        if (proceedButton != null)
        {
            proceedButton.gameObject.SetActive(false);
            Debug.Log("Awake: Button forcefully disabled");
        }
    }

    void Start()
    {
        // Ensure all packages start unselected
        selectedPackageIDs.Clear();
        toSpawnPackages.Clear();
        selection.isOn = false;

        index = 0;
        if (packages != null && packages.Count > 0)
            SetPackagesData();

        // Force hide the proceed button at start
        if (proceedButton != null)
        {
            proceedButton.gameObject.SetActive(false);
            Debug.Log("Start: Button forcefully disabled");
        }
    }

    void OnEnable()
    {
        // Also hide when the script becomes enabled
        if (proceedButton != null)
        {
            proceedButton.gameObject.SetActive(false);
            Debug.Log("OnEnable: Button forcefully disabled");
        }
    }

    void Update()
    {
        ConditionToUnlockProceed();
        //Debug.Log($"toSpawn Count: {toSpawnPackages.Count}, Required: {requiredNumber}, Button Active: {proceedButton.gameObject.activeSelf}");
    }

    void SetPackagesData()
    {
        name.text = packages[index].packageReceipientName;
        reward.text = $"Reward: ${packages[index].packageValue}";

        // Update toggle based on package's own selection state
        selection.isOn = selectedPackageIDs.Contains(packages[index].packageID);
    }

    public void NextButton()
    {
        index++;
        if (index >= packages.Count)
        {
            index = 0;
        }
        SetPackagesData();
    }

    public void BackButton()
    {
        index--;
        if (index < 0)
        {
            index = packages.Count - 1;
        }
        SetPackagesData();
    }

    public void FinishButton()
    {
        clipboard.SetActive(false);

        // Spawn only the selected packages
        foreach (int packageID in toSpawnPackages)
        {
            PackageData packageData = packages.Find(p => p.packageID == packageID);
            if (packageData != null && packageData.type != null)
            {
                SpawnPackageWithData(packageData);
            }
        }

        packagesPicked = true;

        player.GetComponent<PlayerManagement>().enabled = true;
    }

    private void SpawnPackageWithData(PackageData packageData)
    {
        // Instantiate the package prefab
        GameObject packageInstance = Instantiate(packageData.type, packageSpawnPoint.position, packageSpawnPoint.rotation, packageSpawnPoint);

        // Get or add the Package component and set its data
        PackageManager packageComponent = packageInstance.GetComponent<PackageManager>();

        if (packageComponent == null)
        {
            packageComponent = packageInstance.AddComponent<PackageManager>();
        }

        // Set the package data from ScriptableObject
        //packageComponent.recipientName = packageData.packageReceipientName;
        packageComponent.packageValue = packageData.packageValue;
        packageComponent.packageID = packageData.packageID;
        packageComponent.packageHP = packageData.packageHP;

        Debug.Log($"Spawned packageID of  {packageComponent.packageID} with value ${packageComponent.packageValue} and HP of {packageComponent.packageHP}");
    }

    //public void FinishButton()
    //{
    //    clipboard.SetActive(false);
    //    //Start spawning the packages
    //    for (int i = 0; i < packages.Count; i++)
    //    {
    //        Instantiate(packages[i].type, packageSpawnPoint);
    //    }

    //    packagesPicked = true;

    //}

    //IEnumerator SpawnBox()
    //{

    //}

    //private System.Collections.IEnumerator SpawnBoxesRoutine()
    //{
    //    isSpawning = true;
    //    BoxAnimation.Play("Warehouse", 0);
    //    yield return null;

    //    yield return new WaitUntil(() =>
    //    {
    //        AnimatorStateInfo state = BoxAnimation.GetCurrentAnimatorStateInfo(0);
    //        return state.IsName("None");
    //    });

    //    // Spawn the box
    //    package = Instantiate(RealBox, new Vector2(3.82093f, 3.275727f), Quaternion.identity);
    //    //packageList.Add(package);
    //    SpawnedBoxes++;

    //    isSpawning = false;
    //}
    public void ConditionToUnlockProceed()
    {
        if(toSpawnPackages.Count == 2)
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
                Debug.Log($"Package {currentSelection} ADDED. Total: {toSpawnPackages.Count}");
            }
        }
        else
        {
            if (selectedPackageIDs.Contains(currentSelection))
            {
                selectedPackageIDs.Remove(currentSelection);
                toSpawnPackages.Remove(currentSelection);
                Debug.Log($"Package {currentSelection} REMOVED. Total: {toSpawnPackages.Count}");
            }
        }

        Debug.Log("AWAITING TO SPAWN PACKAGES:");
        foreach (int packageID in toSpawnPackages)
        {
            Debug.Log($"Package ID: {packageID}");
        }
    }
}