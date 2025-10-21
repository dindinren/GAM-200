using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Dialogue_NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public List<NewReceipientManager> receipientsManager;

    [Header("LINES")]
    public string[] lines;

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

  

    private void Start()
    {
        playerChoose = false;
        dialogueEnded = false;
        packaageHandovered = false;

        //Set the dialogue box to not show first
        dialogueBox.SetActive(false);

    }
    private void Update()
    {
        Debug.Log($"INDEX NO. {index}");

        if (playerChoose == false && Input.GetMouseButtonDown(0))
        {
            DialogueAdvance();
            NPC_Checker();
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
            //get the current line
            text.text = lines[index];
        }
    }


    public void ButtonAccept()
    {
        NewReceipientManager.checker += 1;

        index = acceptButtonIndexNumber;
        DialogueAdvance();
        NPC_Checker();

        playerChoose = false;
        packaageHandovered = true;
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
            packageAccept.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);

            playerChoose = true;

            Debug.Log("CHOICES APPEAR");
        }

        else if (index == acceptButtonIndexNumber)
        {
            playerPP.SetActive(true);
            NPCPP.SetActive(false);

            text.alignment = TextAlignmentOptions.TopLeft;


        }

        //else if (index == exitButtonIndexNumber)
        //{
        //}


        else
        {
            packageAccept.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);

            playerPP.SetActive(false);
            NPCPP.SetActive(true);

            //playerChoose = false;

            text.alignment = TextAlignmentOptions.TopRight;
        }

    }
    

    public void DialogueAppear()
    {
        text.text = string.Empty;
        StartDialogue();
    }
    void StartDialogue()
    {
        dialogueEnded = false;

        index = 0;
        StartCoroutine(TypeLine());
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
        if (dialogueEnded == false && index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueEnded = true;

            dialogueBox.SetActive(false);
        }
    }


}
