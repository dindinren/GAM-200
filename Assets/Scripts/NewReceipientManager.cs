using UnityEngine;
using System.Collections;
public class NewReceipientManager : MonoBehaviour
{
    private PackageMove packMov;
    public PlayerManagement playerManagement;

    [Header("DIALOGUE BOX")]
    public GameObject dialogueBox;


    [Header("-------------")]
    public bool completed;
    public static bool showResult;
    bool playerIsNear = false;
    int playerCount = 0;
    bool key_is_Pressed;
    

    [Header("RECEIPIENT DATA")]
    public int receipientID;

    private void Awake()
    {
        if (packMov == null)
        {
            packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }
        playerManagement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"checker number: {checker}");
        //Debug.Log($"dialogueended:{Dialogue_NPC.dialogueEnded}");
        
        if(Dialogue_NPC.dialogueEnded == true)
        {
            playerManagement.enabled = true;
        }

        if (dialogueBox.GetComponent<Dialogue_NPC>().packaageHandovered == true)
        {
            CheckIfPackageAndReceipientTheSame();

            //Debug.Log("PACKAGE HANDOVERR");
        }

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            key_is_Pressed = true;

        }
        else
        {
            key_is_Pressed = false;
        }

        DialogueSTART();

    }


    public void DialogueSTART()
    {
        if(playerIsNear && key_is_Pressed)
        {
            if(Dialogue_NPC.count == 0 && playerCount == 0 && !Dialogue_NPC.dialogueEnded)
            {
                DialogBox();
                Dialogue_NPC.count++;
                Debug.Log($"DialogueCount: {Dialogue_NPC.count}");

                Dialogue_NPC.spawned = true;
  
                playerCount++;

            }

        }


    }
    void DialogBox()
    {
        playerManagement.enabled = false;

        dialogueBox.SetActive(true);

        if (Dialogue_NPC.count == 0)
        {
            Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();
            dlog.DialogueAppear();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIsNear = true;

        playerCount = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsNear = false;

        Dialogue_NPC.count = 0;
        Dialogue_NPC.spawned = false;
        Dialogue_NPC.dialogueEnded = false; //just in case for reset idk
        
        playerCount = 1;
    }


    public void CheckIfPackageAndReceipientTheSame()
    {
        Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();

        PlayerManagement playManage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();


        GameObject result = null;
        foreach(GameObject package in packMov.GetAttachedPackagesList())
        {
            if (receipientID == package.GetComponent<PackageManager>().packageID)
            {
                completed = true;
                result = package;

                //add player money
                int moneyGot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageManager>().GetPackageValue();
                playManage.SetMoneySatus(moneyGot);
                
                Debug.Log($"PLAYER GOT ${moneyGot}");
                //Debug.Log($"DESTROYED PACKAGE {package.GetComponent<PackageManager>().packageID} for RECEIPIENT {receipientID}");
                break;
            }
        }
        Destroy(result);
    }
    
}
