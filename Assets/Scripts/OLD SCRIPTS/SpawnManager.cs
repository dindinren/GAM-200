using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> PackagesList = new List<GameObject>(); //consists of the packages type
    //public List<GameObject> ReceipientList = new List<GameObject>(); //consits of different receipients
    GameObject spawnedPackage;
    public void SetRecipientData()
    {

    }
    
    public void SpawnPackageAndReceipient()
    {

    }

    public void SpawnPackage()
    {
        var SpawnLocation = new Vector2(5, 3);
        spawnedPackage = Instantiate(PackagesList[2], SpawnLocation, Quaternion.identity);
    }
    
    public GameObject GetPackageClone()
    {
        return spawnedPackage;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
