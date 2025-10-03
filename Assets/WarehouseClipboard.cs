using UnityEngine;

public class WarehouseClipboard : MonoBehaviour
{
    public GameObject clipboard;
    public GameObject proceedbutton;
    public WarehouseSpawnManager spawnManager;
    public GameObject player;

    private void OnMouseDown()
    {
        if(spawnManager.packagesPicked == false)
        {
            clipboard.SetActive(true);
            proceedbutton.SetActive(false);
            //locked the player in place
            player.GetComponent<PlayerManagement>().enabled = false;
        }
        else
        {
            clipboard.SetActive(false);
        }

    }
}
