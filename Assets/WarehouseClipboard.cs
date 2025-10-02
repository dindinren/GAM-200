using UnityEngine;

public class WarehouseClipboard : MonoBehaviour
{
    public GameObject clipboard;
    public GameObject proceedbutton;
    public WarehouseSpawnManager spawnManager;

    private void OnMouseDown()
    {
        if(spawnManager.packagesPicked == false)
        {
            clipboard.SetActive(true);
            proceedbutton.SetActive(false);
        }
        else
        {
            clipboard.SetActive(false);
        }

    }
}
