using UnityEngine;

public class PackageSpawn : MonoBehaviour
{
    public GameObject package;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPackage()
    {
        GameObject instantiatedObject = Instantiate(package, new Vector3(3, 1, 0), Quaternion.identity);
    }
}
