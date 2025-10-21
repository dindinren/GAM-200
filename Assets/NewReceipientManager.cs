using UnityEngine;
using System.Collections;
public class NewReceipientManager : MonoBehaviour
{
    private PackageMove packMov;
    //private PackageManager packageManager;
    
    public GameObject winScreen; //temp for now until i got a levelmanager up 
    public GameObject dialogueBox;

    [Header("-------------")]
    public static int checker;
    public bool completed;
    private int requiredNumber;

    

    [Header("RECEIPIENT DATA")]
    public int receipientID;

    private void Awake()
    {
        if (packMov == null)
        {
            packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }

        requiredNumber = WarehouseSpawnManager.toSpawnPackages.Count;
        //Debug.Log($"Required Number: {requiredNumber}");


        dialogueBox.SetActive(false);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checker = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"checker number: {checker}");
        //Debug.Log($"dialogueended:{Dialogue_NPC.dialogueEnded}");
        if (Dialogue_NPC.dialogueEnded == true && dialogueBox.GetComponent<Dialogue_NPC>().packaageHandovered == true)
        {

            CheckIfPackageAndReceipientTheSame();

            Debug.Log("PACKAGE HANDOVERR");
            //checker += 1;
        }
        StartCoroutine(IWantThePackageGoneB4TheWinScreenAppear());

    }
    IEnumerator IWantThePackageGoneB4TheWinScreenAppear()
    {
        yield return new WaitForSeconds(1);
        if (Dialogue_NPC.dialogueEnded == true)
        {
            PackagesCompleted();
        }
    }
    private void OnMouseDown()
    {
        dialogueBox.SetActive(true);

        Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();
        dlog.DialogueAppear();



        //StartCoroutine(NPCDialogue());
    }

    public int returnReceipientID()
    {
        return receipientID;
    }

    //IEnumerator NPCDialogue()
    //{


    //    yield return new WaitForSeconds(1);

    //}

    public void CheckIfPackageAndReceipientTheSame()
    {
        Dialogue_NPC dlog = dialogueBox.GetComponent<Dialogue_NPC>();
        Debug.Log($"Checker: {checker}");

        GameObject result = null;
        foreach(GameObject package in packMov.GetAttachedPackagesList())
        {
            if (receipientID == package.GetComponent<PackageManager>().packageID && dlog.packaageHandovered == true)
            {
                completed = true;
                result = package;
               
                Debug.Log($"DESTROYED PACKAGE {package.GetComponent<PackageManager>().packageID} for RECEIPIENT {receipientID}");
                break;
            }
        }
        Destroy(result);
        SoundManager.PlaySound(SoundType.DELIVERYCOMPLETE);
    }

    public void PackagesCompleted()
    {
        if(checker == requiredNumber)
        {
            SoundManager.PlaySound(SoundType.DAYCOMPLETE);

            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
