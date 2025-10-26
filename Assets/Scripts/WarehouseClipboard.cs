using UnityEngine;

public class WarehouseClipboard : MonoBehaviour
{
    public GameObject clipboard;
    public GameObject player;

    bool key_Is_Pressed;
    bool playerNear;
    //int count;

    private void Start()
    {
        clipboard.SetActive(false);

        //count = 0;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            key_Is_Pressed = true;
            ClipboardChecker();
        }
        else
        {
            key_Is_Pressed = false;
        }
    }

    void ClipboardChecker()
    {
        if (WarehouseSpawnManager.playerReadyToGo == false && playerNear)
        {
            clipboard.SetActive(true);
            player.GetComponent<PlayerManagement>().enabled = false; //locked the player in place
            //count++;
        }
        else
        {
            clipboard.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerNear = true;
        Debug.Log("player is near");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerNear = false;
    }

}
