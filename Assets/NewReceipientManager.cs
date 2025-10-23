using UnityEngine;
using System.Collections;
public class NewReceipientManager : MonoBehaviour
{
    private PackageMove packMov;
    public PlayerManagement playerManagement;
    //private PackageManager packageManager;
    
    public GameObject winScreen; //temp for now until i got a levelmanager up 
    public GameObject dialogueBox;

    public Collider2D receipientCollider;

    [Header("-------------")]
    public bool completed;
    public static bool showResult;
    public static bool dialogueStart;
    //bool NPCCome = false;

    [Header("RECEIPIENT DATA")]
    public int receipientID;

    private void Awake()
    {
        if (packMov == null)
        {
            packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }
        receipientCollider = GetComponent<Collider2D>();

        playerManagement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();


        dialogueBox.SetActive(false);

        dialogueStart = false;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //checker = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"checker number: {checker}");
        //Debug.Log($"dialogueended:{Dialogue_NPC.dialogueEnded}");
        if (dialogueBox.GetComponent<Dialogue_NPC>().packaageHandovered == true)
        {
            CheckIfPackageAndReceipientTheSame();

            Debug.Log("PACKAGE HANDOVERR");
            //checker += 1;
        }
        if(Dialogue_NPC.dialogueEnded == true)
        {
            receipientCollider.enabled = true;
            playerManagement.enabled = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //NPCCome = true;
    }

    //IEnumerator test()
    //{
    //    dialogueBox.SetActive(true);
    //    yield return new WaitForSeconds(3);

    //    Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();
    //    dlog.StartDialogue();
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log($"NPCCome: {NPCCome}");
        if (Input.GetKey(KeyCode.F) && (collision.gameObject.tag == "Player"))
        {
            dialogueBox.SetActive(true);

            Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();
            dlog.StartDialogue();

            //StartCoroutine(test());

            playerManagement.enabled = false;

            dialogueStart = true;

            //receipientCollider.enabled = false;

            //PlayerManagement playManage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
            //playManage.interactionButton.SetActive(true);


        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueStart = false;
        Debug.Log("BECOME FAUST");
    }

    public int returnReceipientID()
    {
        return receipientID;
    }


    public void CheckIfPackageAndReceipientTheSame()
    {
        showResult = false;
        Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();
        //Debug.Log($"Checker: {checker}");

        PlayerManagement playManage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();


        GameObject result = null;
        foreach(GameObject package in packMov.GetAttachedPackagesList())
        {
            if (receipientID == package.GetComponent<PackageManager>().packageID && dlog.packaageHandovered == true)
            {
                completed = true;
                result = package;

                //add player money
                int moneyGot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageManager>().GetPackageValue();
                playManage.SetMoneySatus(moneyGot);
                
                Debug.Log($"PLAYER GOT ${moneyGot}");
                Debug.Log($"DESTROYED PACKAGE {package.GetComponent<PackageManager>().packageID} for RECEIPIENT {receipientID}");
                break;
            }
        }
        Destroy(result);
        SoundManager.PlaySound(SoundType.DELIVERYCOMPLETE);
        showResult = true;
    }

    
}
