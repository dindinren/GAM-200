using UnityEngine;

public class ReceipientManager : MonoBehaviour
{
    /// <summary>
    /// Handles receipient receiving package
    /// </summary>

    public PlayerManagement playerManagement; //get from player
    public PackageMove packMov; //get from the player's trigger area

    //SpawnManager spawnManager;
    ConveyerBelt conveyer;
    GameObject prefab;
    GameObject belt;
    private void Awake()
    {
        playerManagement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
        packMov = GameObject.FindGameObjectWithTag("Paddle").GetComponentInChildren<PackageMove>();
        belt = GameObject.FindGameObjectWithTag("Belt");
        conveyer = belt.GetComponent<ConveyerBelt>();
        belt.SetActive(false);
        //spawnManager = GameObject.Find("Canvas").GetComponent<SpawnManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnMouseDown()
    {
        GameObject package = GameObject.FindGameObjectWithTag("Package");
        if (packMov.GetAttachedPackagesList().Contains(package))

        prefab = conveyer.GetPackageCloneCB();
        int packMan = prefab.GetComponent<PackageManager>().GetPackageValue();

        playerManagement.SetMoneySatus(packMan);

        Destroy(package);

        //play sound
        SoundManager.PlaySound(SoundType.DELIVERYCOMPLETE);


    }
}


