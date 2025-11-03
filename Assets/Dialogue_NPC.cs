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

    private void Awake()
    {
        if(packMov == null)
        {
            packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }

    }
    private void Start()
    { 
        playerChoose = false;
        dialogueEnded = false;
        packaageHandovered = false;

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
            packageAccept.gameObject.SetActive(false);

            foreach (GameObject p in packMov.GetAttachedPackagesList())
            {
                if (p.GetComponent<PackageManager>().packageID == dialogue_ID)
                {
                    packageAccept.gameObject.SetActive(true);
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
    }


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

        SoundManager.PlaySound(SoundType.DELIVERYCOMPLETE);
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
