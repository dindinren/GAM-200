using UnityEngine;


public class PlayerPackageCheck : MonoBehaviour
{
    /// <summary>
    /// Check if the player has any packages on top before heading out
    /// </summary>

    public GameObject invisibleWall;
    public GameObject SceneChangeTriggerArea;
    public PackageMove PackageMove;

    public GameObject dialogueBox;
    //public PlayerManagement player;

    ///very specific bug fix that prob have a better fix but i too lazy
    //public GameObject PackageTriggerArea;

    int count;

    private void Awake()
    {
        PackageMove = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;

        SceneChangeTriggerArea.SetActive(false);
        invisibleWall.SetActive(true);
    }

    private void FixedUpdate()
    {
        //if (PackageMove.GetAttachedPackagesList().Count != WarehouseSpawnManager.requiredNumber)
        //{
        //    if (Dialogue.dialogueEnded) //idk if this will fuck up the framw rate im sorry
        //    {
        //        Debug.Log("DIALOGUE ENDED");
        //        //player can move again
        //        //player.enabled = true;

        //        //PackageTriggerArea.layer = LayerMask.NameToLayer("Default");     //allow player to interact with the PackageTriggerArea
        //    }
            
        //}
        //if(Dialogue.dialogueEnded == false)
        //{
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PackageMove.GetAttachedPackagesList().Count != WarehouseSpawnManager.requiredNumber && Dialogue.dialogueEnded == false)
            {
                DialogueWarn();  
            }
            Checker();
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

    public void DialogueWarn()
    {
        //PackageTriggerArea.layer = LayerMask.NameToLayer("Ignore Raycast");         //Set the PackageTriggerArea to ignore raycast until finish

        dialogueBox.SetActive(true);

        Dialogue dlog = dialogueBox.GetComponent<Dialogue>();
        dlog.DialogueAppear();

        ////Disbale player movement
        //player.enabled = false;
    }


}
