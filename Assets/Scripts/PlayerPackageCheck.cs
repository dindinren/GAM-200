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
    public PlayerManagement player;

    ///very specific bug fix that prob have a better fix but i too lazy
    //public GameObject PackageTriggerArea;

    //int count;

    private void Awake()
    {
        PackageMove = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dialogue.count = 0;

        SceneChangeTriggerArea.SetActive(false);
        invisibleWall.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (Dialogue.dialogueEnded)
        {
            player.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Checker();

        if (PackageMove.GetAttachedPackagesList().Count != WarehouseSpawnManager.requiredNumber)
        {
            if (collision.gameObject.tag == "Player" && Dialogue.count == 0)
            {
                player.enabled = false;
                DialogueWarn();
                Dialogue.count++;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Dialogue.dialogueEnded = false;
        Dialogue.count = 0;
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

        ////Disbale player movement
        //player.enabled = false;

        Animator anim = player.GetComponent<Animator>();
        anim.SetBool("Walk", false);

        dialogueBox.SetActive(true);

        if(Dialogue.count == 0)
        {
            Dialogue dlog = dialogueBox.GetComponent<Dialogue>();
            dlog.DialogueAppear();
        }

    }


}
