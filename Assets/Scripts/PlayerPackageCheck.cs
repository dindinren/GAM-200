using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.ReloadAttribute;

public class PlayerPackageCheck : MonoBehaviour
{
    /// <summary>
    /// Check if the player has any packages on top before heading out
    /// </summary>

    public GameObject invisibleWall;
    public GameObject SceneChangeTriggerArea;
    public PackageMove PackageMove;

    public GameObject dialogueBox;
    public PlayerManagement player;

    ///very specific bug fix that prob have a better fix but i too lazy
    public GameObject PackageTriggerArea;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PackageMove = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();


        SceneChangeTriggerArea.SetActive(false);
        invisibleWall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PackageMove.GetAttachedPackagesList().Count != WarehouseSpawnManager.requiredNumber)
        {
            if (Dialogue.dialogueEnded == true) //idk if this will fuck up the framw rate im sorry
            {
                Debug.Log("DIALOGUE ENDED");
                //player can move again
                player.enabled = true;

                //allow player to interact with the PackageTriggerArea
                PackageTriggerArea.layer = LayerMask.NameToLayer("Default");
            }
        }

    }

    public void Checker()
    {
        Debug.Log($"packages: {PackageMove.GetAttachedPackagesList().Count} and required: {WarehouseSpawnManager.requiredNumber}");
        if (PackageMove.GetAttachedPackagesList().Count == WarehouseSpawnManager.requiredNumber)
        {
            SceneChangeTriggerArea.SetActive(true);
            invisibleWall.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PackageMove.GetAttachedPackagesList().Count != WarehouseSpawnManager.requiredNumber)
        {
            DialogueWarn();
        }
        Checker();


    }

    public void DialogueWarn()
    {
        //Set the PackageTriggerArea to ignore raycast until finish
        PackageTriggerArea.layer = LayerMask.NameToLayer("Ignore Raycast");

        dialogueBox.SetActive(true);

        Dialogue dlog = dialogueBox.GetComponent<Dialogue>();
        dlog.DialogueAppear();

        //Disbale player movement
        player.enabled = false;
    }


}
