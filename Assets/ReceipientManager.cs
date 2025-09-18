using UnityEngine;

public class ReceipientManager : MonoBehaviour
{
    /// <summary>
    /// Handles receipient receiving package
    /// </summary>

    public PlayerManagement playerManagement; //get from player
    public PackageMove packMov; //get from the player's trigger area

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnMouseDown()
    {
        GameObject package = GameObject.FindGameObjectWithTag("Package");
        if (packMov.GetAttachedPackagesList().Contains(package))
        {
            playerManagement.SetMoneySatus(20); 
            Destroy(package);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
