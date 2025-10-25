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
    bool F_is_Pressed;
    

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
            playerManagement.enabled = true;
        }

    }

   void DialogueSTART()
    {
        if (playerIsNear && F_is_Pressed && playerCount == 0)
        {
            if (dialogueBox.GetComponent<Dialogue_NPC>().dialogue_ID == receipientID)
            {
                dialogueBox.SetActive(true);
                dialogueBox.GetComponent<Dialogue_NPC>().StartDialogue();
                playerCount++;
                //StartCoroutine(Delay_playerCount());

                Debug.Log($"PlayerCount = {playerCount}");
            }

        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            F_is_Pressed = true;
            DialogueSTART();

        }
        else
        {
            F_is_Pressed = false;
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
        playerCount = 1;
    }

    public int returnReceipientID()
    {
        return receipientID;
    }


    public void CheckIfPackageAndReceipientTheSame()
    {
        Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();

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
                //Debug.Log($"DESTROYED PACKAGE {package.GetComponent<PackageManager>().packageID} for RECEIPIENT {receipientID}");
                break;
            }
        }
        Destroy(result);
        SoundManager.PlaySound(SoundType.DELIVERYCOMPLETE);
    }
    
}
