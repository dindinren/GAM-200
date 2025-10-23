using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Dialogue_NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public List<NewReceipientManager> receipientsManager;
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
    private bool playerChoose;
    private bool isTyping;

    private void Awake()
    {
        if(packMov == null)
        {
            packMov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }

        //StopAllCoroutines();
        NPC_Checker();

    }
    private void Start()
    {
        playerChoose = false;
        dialogueEnded = false;
        packaageHandovered = false;

        index = 0;

        //Set the dialogue box to not show first
        dialogueBox.SetActive(false);


    }
    private void Update()
    {
        //Debug.Log($"isTyping: {isTyping}");
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (playerChoose == false)
            {
                DialogueAdvance();
                NPC_Checker();
            }
        }
        Debug.Log($"INDEX NO. {index}");



    }

    public void DialogueAdvance()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Advance to the next line
            if (text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                //text.text = string.Empty;
                text.text = lines[index];
            }

        }

    }


    public void ButtonAccept()
    {
        index = acceptButtonIndexNumber-1;
        NPC_Checker();
        NextLine();
        NPC_Checker();


        playerChoose = false;
        packaageHandovered = true;

        packageAccept.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);   

        DayManager.checker += 1;

    }
    public void ButtonExit()
    {
        index = exitButtonIndexNumber;
        DialogueAdvance();
        NPC_Checker();

        playerChoose = false;
    }

    public void NPC_Checker()
    {
        if (index == interactionChooseIndexNumber)
        {
            foreach(GameObject p in packMov.GetAttachedPackagesList())
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
            packaageHandovered = true;

            playerPP.SetActive(true);
            NPCPP.SetActive(false);

            text.alignment = TextAlignmentOptions.TopLeft;

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
        if(NewReceipientManager.dialogueStart == false)
        {
            text.text = string.Empty;
            index = 0;
            StartCoroutine(TypeLine());
            dialogueEnded = false;
        }

        //isTyping = false;
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
        StopAllCoroutines();

        if (dialogueEnded == false && index < lines.Length - 1)
        {
            index++;

            text.text = string.Empty;

            StartCoroutine(TypeLine());
            //isTyping = false;
        }
        else
        {
            dialogueEnded = true;

            dialogueBox.SetActive(false);
        }
    }


}
