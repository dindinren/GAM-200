using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Dialogue_NPC : MonoBehaviour
{
    [Header("OBJECTS")]
    public GameObject dialogueBox_player;
    public GameObject dialogueBox_NPC;
    public GameObject dialogueBox;
    public GameObject continueButton;
    private PackageMove packMov;

    [Header("LINES")]
    public string[] lines;
    public int dialogue_ID;

    [Header("BUTTONS")]
    public Button packageAccept;
    public Button exit;

    [Header("POTRAITS")]
    public GameObject playerPP;
    public GameObject NPCPP;

    [Header("TEXT ADJUSTMENTS")]
    public int acceptButtonIndexNumber;
    public int exitButtonIndexNumber;
    public int interactionChooseIndexNumber;
    public TextMeshProUGUI text;
    public float textSpeed;

    [Header("SHARED VARIABLES")]
    public static bool dialogueEnded;
    public static bool spawned;
    public static int count;

    [Header("-----------")]
    public bool packaageHandovered;
    bool playerChoose; //temp stop the dialogue from moving to allow players to choose
    bool key_is_Pressed;
    int keyPresses;
    private int index;
    //bool nextLinePls;

    private void Awake()
    {
        if(packMov == null)
        {
            packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }

        //StopAllCoroutines();

    }
    private void Start()
    { 
        playerChoose = false;
        dialogueEnded = false;
        packaageHandovered = false;
        //nextLinePls = false;

        NPC_Checker();

        //Set the dialogue box to not show first
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
        
    }

    private void FixedUpdate()
    {
        if (text.text == lines[index])
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            key_is_Pressed = true;
            keyPresses++;
        }
        else
        {
            key_is_Pressed = false;
            keyPresses = 0;
        }

        Advance();

        //Debug.Log($"dialogueEnded: {dialogueEnded}");
    }


    public void NPC_Checker()
    {
        if (index == interactionChooseIndexNumber)
        {
            foreach (GameObject p in packMov.GetAttachedPackagesList())
            {
                if (p.GetComponent<PackageManager>().packageID == dialogue_ID)
                {
                    packageAccept.gameObject.SetActive(true);
                }
                else
                {
                    packageAccept.gameObject.SetActive(false);
                }

                exit.gameObject.SetActive(true);
            }

            playerChoose = true;

            Debug.Log("CHOICES APPEAR");
        }

        else if (index == acceptButtonIndexNumber)
        {
            playerPP.SetActive(true);
            NPCPP.SetActive(false);

            dialogueBox_player.SetActive(false);
            dialogueBox_NPC.SetActive(true);

            text.alignment = TextAlignmentOptions.TopLeft;

            packaageHandovered = true;
            //StartCoroutine(Delay_PackageHandover());
        }
        else if (index == acceptButtonIndexNumber + 1)
        {
            packaageHandovered = false;

            playerPP.SetActive(false);
            NPCPP.SetActive(true);

            dialogueBox_player.SetActive(false);
            dialogueBox_NPC.SetActive(true);

            text.alignment = TextAlignmentOptions.TopRight;

        }

        else
        {
            dialogueBox_player.SetActive(false);
            dialogueBox_NPC.SetActive(true);

            packageAccept.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);

            playerPP.SetActive(false);
            NPCPP.SetActive(true);

            text.alignment = TextAlignmentOptions.TopRight;

        }



    }

    void Advance()
    {
        if(key_is_Pressed && keyPresses == 1 && playerChoose == false && dialogueEnded == false)
        {
            if(text.text == lines[index])
            {
                NextLine();
                Debug.Log("Next Line");
                NPC_Checker();
                Debug.Log("NPC Checker");
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
                NPC_Checker();
            }
        }
        //if (playerChoose == false && F_is_Pressed && dialogueEnded == false)
        //{
        //    Debug.Log($"nextLinePls: {nextLinePls}");

        //    Debug.Log($"INDEX NO. {index}");

        //    if (nextLinePls)
        //    {
        //        DialogueAdvance();
        //        Debug.Log("Dialogue Advance");
        //        NPC_Checker();
        //        Debug.Log("NPC Checker");
        //    }
        //    else
        //    {
        //        nextLinePls = true;
        //        //StartCoroutine(Delay_nextLinePls());
        //        Debug.Log("DELAY FINSIH");
        //    }
        //}
    }

    //public void DialogueAdvance()
    //{
    //    //Advance to the next line
    //    if (text.text == lines[index])
    //    {
    //        NextLine();
    //    }
    //    else
    //    {
    //        StopAllCoroutines();
    //        text.text = lines[index];
    //    }

    //}

    #region Buttons
    public void ButtonAccept()
    {
        index = acceptButtonIndexNumber - 1;
        NPC_Checker();
        NextLine();
        NPC_Checker();

        /// ---- UI Stuff ---- \\\
        dialogueBox_NPC.SetActive(false);
        dialogueBox_player.SetActive(true);
        
        packageAccept.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);


        playerChoose = false;
        DayManager.checker += 1; //prob put somewhere else

    }
    public void ButtonExit()
    {
        index = exitButtonIndexNumber - 1;
        NPC_Checker();
        NextLine();
        NPC_Checker();

        playerChoose = false;
    }
    #endregion 

    public void DialogueAppear()
    {
        text.text = string.Empty;
        StartDialogue();
    }
    void StartDialogue()
    {
        count = 0;

        if (!spawned)
        {
            if (count == 0)
            {
                dialogueEnded = false;
                index = 0;

                StartCoroutine(TypeLine());

            }

            Debug.Log($"Dialogue Count: {Dialogue.count}");
            Debug.Log($"DIALOGUE SPAWNED {spawned}");
        }
    }

    //public void StartDialogue()
    //{
    //    text.text = string.Empty;
    //    index = 0;

    //    StartCoroutine(TypeLine());

    //    dialogueEnded = false;
    //    //nextLinePls = true;

    //    Debug.Log("DIALOGUE HAS BEEN STARTED");
    //}

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        continueButton.SetActive(true);
    }

    void NextLine()
    {
        //StopAllCoroutines();

        if (index < lines.Length - 1 && dialogueEnded == false)
        {
            StopAllCoroutines();

            continueButton.SetActive(false);

            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());

            Debug.Log("NEXT LINE?");
        }
        else
        {
            dialogueEnded = true;
            //index = 0;
            dialogueBox.SetActive(false);
            //dialogueBox_NPC.SetActive(false);
            count = 0;
            Debug.Log("DIALOGUE IS FINISHED");


        }
    }


}
