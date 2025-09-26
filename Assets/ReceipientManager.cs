using UnityEngine;

public class ReceipientManager : MonoBehaviour
{
    /// <summary>
    /// Handles receipient receiving package
    /// </summary>

    public PlayerManagement playerManagement; //get from player
    public PackageMove packMov; //get from the player's trigger area

    SpawnManager spawnManager;
    GameObject prefab;

    private void Awake()
    {
        spawnManager = GameObject.Find("Canvas").GetComponent<SpawnManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnMouseDown()
    {
        GameObject package = GameObject.FindGameObjectWithTag("Package");
        if (packMov.GetAttachedPackagesList().Contains(package))
        {
            prefab = spawnManager.GetPackageClone();
            int packMan = prefab.GetComponent<PackageManager>().GetPackageValue();

            playerManagement.SetMoneySatus(packMan); 

            Destroy(package);

            //play sound
            SoundManager.PlaySound(SoundType.DELIVERYCOMPLETE);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
