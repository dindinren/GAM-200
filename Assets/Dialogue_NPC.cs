using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Dialogue_NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    //public List<NewReceipientManager> receipientsManager;
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


    [Header("-----------")]
    private int index;
    public static bool dialogueEnded;
    public bool packaageHandovered;
    //int packageHandovered_Presses;
    private bool playerChoose;
    bool F_is_Pressed;
    bool nextLinePls;
    bool lineFinished;

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
        nextLinePls = false;

        NPC_Checker();

        //Set the dialogue box to not show first
        dialogueBox.SetActive(false);

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            F_is_Pressed = true;
        }
        else
        {
            F_is_Pressed = false;
        }
        DialogueManager();

    }



    void DialogueManager()
    {
        if (playerChoose == false && F_is_Pressed && dialogueEnded == false)
        {
            Debug.Log($"nextLinePls: {nextLinePls}");

            Debug.Log($"INDEX NO. {index}");

            if (nextLinePls)
            {
                DialogueAdvance();
                Debug.Log("Dialogue Advance");
                NPC_Checker();
                Debug.Log("NPC Checker");
            }
            else
            {
                nextLinePls = true;
                //StartCoroutine(Delay_nextLinePls());
                Debug.Log("DELAY FINSIH");
            }
        }
    }

    public void DialogueAdvance()
    {
        //Advance to the next line
        if (text.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            text.text = lines[index];
        }

    }

    #region Buttons
    public void ButtonAccept()
    {
        index = acceptButtonIndexNumber - 1;
        NPC_Checker();
        NextLine();
        NPC_Checker();

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

    public void NPC_Checker()
    {
        if (index == interactionChooseIndexNumber)
        { 
            foreach (GameObject p in packMov.GetAttachedPackagesList())
            {
                if(p.GetComponent<PackageManager>().packageID == dialogue_ID)
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
            text.alignment = TextAlignmentOptions.TopLeft;

            packaageHandovered = true;
            //StartCoroutine(Delay_PackageHandover());
        }
        else if (index == acceptButtonIndexNumber + 1)
        {
            packaageHandovered = false;
            playerPP.SetActive(false);
            NPCPP.SetActive(true);
        }

        else
        {
            packageAccept.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);

            playerPP.SetActive(false);
            NPCPP.SetActive(true);

            text.alignment = TextAlignmentOptions.TopRight;

        }

    }



    public void StartDialogue()
    {
        text.text = string.Empty;
        index = 0;

        StartCoroutine(TypeLine());
        
        dialogueEnded = false;
        nextLinePls = true;

        Debug.Log("DIALOGUE HAS BEEN STARTED");
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        //StopAllCoroutines();

        if (dialogueEnded == false && index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
            Debug.Log("NEXT LINE?");
            //isTyping = false;
        }
        else
        {
            dialogueBox.SetActive(false);
            dialogueEnded = true;
            index = 0;
            Debug.Log("DIALOGUE IS FINISHED");


        }
    }


}
